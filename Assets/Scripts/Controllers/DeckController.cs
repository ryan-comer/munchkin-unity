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
                Card returnCard = currentDoorDeck[0];
                returnCard.gameObject.SetActive(true);

                currentDoorDeck.RemoveAt(0);
                return returnCard;
        }

        return null;
    }

    // Create the card objects for each card
    private void initializeDecks()
    {
        // Initialize door deck
        foreach(Card card in doorDeck)
        {
            var cardObj = Instantiate(card, doorDeckLocation.position, Quaternion.identity);
            cardObj.gameObject.SetActive(false);    // Cards in the deck are hidden
            currentDoorDeck.Add(cardObj);
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
