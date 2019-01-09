using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeckController : NetworkBehaviour
{

    public Card[] doorDeck;    // The deck of door cards
    public Card[] treasureDeck;    // The deck of treasure cards
    public Card[] playerDeck;  // The deck of player cards

    public Transform doorDeckLocation;
    public Transform treasureDeckLocation;

    public Material[] playerMaterials;  // The materials to apply to the border of cards for each player

    public DiscardPile doorDiscardPile;
    public DiscardPile treasureDiscardPile;

    private List<Card> currentDoorDeck = new List<Card>();
    private List<Card> currentTreasureDeck = new List<Card>();
    private List<Card> currentPlayerDeck = new List<Card>();

    // Maximum cards for each deck
    [SyncVar]
    private int doorDeckStartingSize;
    [SyncVar]
    private int treasureDeckStartingSize;
    [SyncVar]
    private int currentDoorDeckSize;
    [SyncVar]
    private int currentTreasureDeckSize;

    // Optimization - only update deck size if these variables changed
    private int lastCurrentDoorSize = 0;
    private int lastCurrentTreasureDeckSize = 0;

    public static DeckController instance;

    // Card types for the game
    public enum CardType{
        Door,
        Treasure,
        Player
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Only run on server
        if (NetworkServer.active)
        {
            initializeDecks();
            doorDeckStartingSize = currentDoorDeck.Count;
            treasureDeckStartingSize = currentTreasureDeck.Count;
        }
    }

    private void Update()
    {
        // Update the size of each deck
        updateDeckSize(CardType.Door);
        updateDeckSize(CardType.Treasure);
    }

    // Shuffle the appropriate deck
    public void ShuffleDeck(CardType cardType){
        switch (cardType)
        {
            case CardType.Door:
                currentDoorDeck.Shuffle<Card>();
                break;
            case CardType.Treasure:
                currentTreasureDeck.Shuffle<Card>();
                break;
        }
    }

    // Draw the top card from the top of the deck
    public Card DrawCard(CardType cardType, int playerID){
        switch (cardType)
        {
            case CardType.Door:
                // The door deck is empty
                if (currentDoorDeck.Count == 0)
                {
                    return null;
                }

                Card returnCard = currentDoorDeck[0];
                returnCard.RpcEnableCard();

                // Set the border materials
                returnCard.RpcSetBorderColor(playerID);

                currentDoorDeck.RemoveAt(0);
                currentDoorDeckSize = currentDoorDeck.Count;
                return returnCard;
            case CardType.Treasure:
                // Treasure deck is empty
                if(currentTreasureDeck.Count == 0)
                {
                    return null;
                }

                returnCard = currentTreasureDeck[0];
                returnCard.RpcEnableCard();

                // Set the border materials
                returnCard.RpcSetBorderColor(playerID);

                currentTreasureDeck.RemoveAt(0);
                currentTreasureDeckSize = currentTreasureDeck.Count;
                return returnCard;

        }

        return null;
    }

    // Put the card back on the top of the deck
    public void ReturnCard(Card card)
    {
        switch (card.cardType)
        {
            case CardType.Door:
                currentDoorDeck.Insert(0, card);
                card.transform.position = doorDeckLocation.position;
                card.gameObject.SetActive(false);
                //updateDeckSize(CardType.Door);
                break;
            case CardType.Treasure:
                currentTreasureDeck.Insert(0, card);
                card.transform.position = treasureDeckLocation.position;
                card.gameObject.SetActive(false);
                //updateDeckSize(CardType.Treasure);
                break;
        }
    }

    // Get the maximum number of cards for the card type
    public int GetMaxCards(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.Door:
                return doorDeckStartingSize;
            case CardType.Treasure:
                return treasureDeckStartingSize;
        }

        return 0;
    }

    // Add the card to the discard pile
    /*public void DiscardCard(Card card)
    {
        switch (card.cardType)
        {
            case CardType.Door:
                card.transform.position = doorDeckLocation.position;
                card.gameObject.SetActive(false);
                doorDiscardPile.AddCard(card);
                break;
            case CardType.Treasure:
                card.transform.position = treasureDeckLocation.position;
                card.gameObject.SetActive(false);
                treasureDiscardPile.AddCard(card);
                break;
        }
    }*/

    // Create the card objects for each card
    // Send the results to each client
    private void initializeDecks()
    {
        // Initialize door deck
        foreach (Card card in doorDeck)
        {
            var cardObj = Instantiate(card, doorDeckLocation.position, Quaternion.identity);
            cardObj.cardType = CardType.Door;   // Type of this card
            NetworkServer.Spawn(cardObj.gameObject);

            currentDoorDeck.Add(cardObj);
        }
        currentDoorDeckSize = currentDoorDeck.Count;

        // Initialize the treasure deck
        foreach(Card card in treasureDeck)
        {
            var cardObj = Instantiate(card, treasureDeckLocation.position, Quaternion.identity);
            cardObj.cardType = CardType.Treasure;
            NetworkServer.Spawn(cardObj.gameObject);

            currentTreasureDeck.Add(cardObj);
        }
        currentTreasureDeckSize = currentTreasureDeck.Count;

    }

    // Update the height of the deck
    private void updateDeckSize(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.Door:
                // If there is no change, don't update
                if(currentDoorDeckSize == lastCurrentDoorSize)
                {
                    return;
                }

                float newScaleY = (float)currentDoorDeckSize / (float)doorDeckStartingSize;
                doorDeckLocation.localScale = new Vector3(doorDeckLocation.localScale.x, newScaleY, doorDeckLocation.localScale.z);

                lastCurrentDoorSize = currentDoorDeckSize;
                break;
            case CardType.Treasure:
                // If there is no change, don't update
                if (currentTreasureDeckSize == lastCurrentTreasureDeckSize)
                {
                    return;
                }

                newScaleY = (float)currentTreasureDeckSize / (float)treasureDeckStartingSize;
                treasureDeckLocation.localScale = new Vector3(treasureDeckLocation.localScale.x, newScaleY, treasureDeckLocation.localScale.z);

                lastCurrentTreasureDeckSize = currentTreasureDeckSize;
                break;
        }
    }

}

// Extension methods for lists
public static class IListExtensions
{
    // Shuffle the list
    public static void Shuffle<Card>(this IList<Card> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
