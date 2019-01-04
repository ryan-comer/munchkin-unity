using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    public Card[] doorDeck;    // The deck of door cards
    public Card[] treasureDeck;    // The deck of treasure cards
    public Card[] playerDeck;  // The deck of player cards

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

        currentDoorDeck = new List<Card>(doorDeck);
        currentTreasureDeck = new List<Card>(treasureDeck);
        currentPlayerDeck = new List<Card>(playerDeck);
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
                currentDoorDeck.RemoveAt(0);

                return returnCard;
        }

        return null;
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
