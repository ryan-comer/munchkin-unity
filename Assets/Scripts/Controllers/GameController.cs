using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{

    public Dice dice;   // Handle to the dice

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Shuffle all of the decks
    private void shuffleAllDecks()
    {
        DeckController.instance.ShuffleDeck(DeckController.CardType.Door);
        DeckController.instance.ShuffleDeck(DeckController.CardType.Treasure);
        DeckController.instance.ShuffleDeck(DeckController.CardType.Player);
    }
    
}
