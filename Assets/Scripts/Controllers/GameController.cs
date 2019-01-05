﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        shuffleAllDecks();

        HandController.instance.AddCardToHand(DeckController.instance.DrawCard(DeckController.CardType.Door));
        HandController.instance.AddCardToHand(DeckController.instance.DrawCard(DeckController.CardType.Door));
        HandController.instance.AddCardToHand(DeckController.instance.DrawCard(DeckController.CardType.Door));
    }

    // Update is called once per frame
    void Update()
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