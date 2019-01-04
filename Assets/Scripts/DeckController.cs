using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    public Card[] doorDeck;    // The deck of door cards
    public Card[] treasureDeck;    // The deck of treasure cards
    public Card[] playerDeck;  // The deck of player cards

    // Card types for the game
    public enum CardType{
        Door,
        Treasure,
        Player
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Shuffle the appropriate deck
    public void ShuffleDeck(CardType cardType){
        
    }

    // Draw the top card from the top of the deck
    public Card DrawCard(CardType cardType){
        return new Card();
    }

}
