using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    public Card[] doorDeck;    // The deck of door cards
    public Card[] treasureDeck;    // The deck of treasure cards
    public Card[] playerDeck;  // The deck of player cards

    public Transform doorDeckLocation;
    public Transform treasureDeckLocation;

    private List<Card> currentDoorDeck = new List<Card>();
    private List<Card> currentTreasureDeck = new List<Card>();
    private List<Card> currentPlayerDeck = new List<Card>();

    private int doorDeckStartingSize;

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

        initializeDecks();
    }

    private void Start()
    {
        doorDeckStartingSize = currentDoorDeck.Count;
    }

    // Shuffle the appropriate deck
    public void ShuffleDeck(CardType cardType){
        switch (cardType)
        {
            case CardType.Door:
                currentDoorDeck.Shuffle<Card>();
                break;
        }
    }

    // Draw the top card from the top of the deck
    public Card DrawCard(CardType cardType){
        switch (cardType)
        {
            case CardType.Door:
                // The door deck is empty
                if (currentDoorDeck.Count == 0)
                {
                    return null;
                }

                Card returnCard = currentDoorDeck[0];
                returnCard.gameObject.SetActive(true);

                currentDoorDeck.RemoveAt(0);
                updateDeckSize(cardType);
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
                updateDeckSize(CardType.Door);
                break;
        }
    }

    // Create the card objects for each card
    private void initializeDecks()
    {
        // Initialize door deck
        foreach(Card card in doorDeck)
        {
            var cardObj = Instantiate(card, doorDeckLocation.position, Quaternion.identity);
            cardObj.cardType = CardType.Door;   // Type of this card
            cardObj.gameObject.SetActive(false);    // Cards in the deck are hidden
            currentDoorDeck.Add(cardObj);
        }
    }

    // Update the height of the deck
    private void updateDeckSize(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.Door:
                float newScaleY = (float)currentDoorDeck.Count / (float)doorDeckStartingSize;
                doorDeckLocation.localScale = new Vector3(doorDeckLocation.localScale.x, newScaleY, doorDeckLocation.localScale.z);
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
