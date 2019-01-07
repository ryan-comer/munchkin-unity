using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeckController))]
public class SetupDeckEditor : Editor
{

    private string doorPrefabsPath = @"Prefabs/Cards/Door Deck";
    private string treasurePrefabsPath = @"Prefabs/Cards/Treasure Deck";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DeckController deckController = (DeckController)target;
        if (GUILayout.Button("Setup Door Deck"))
        {
            int numItems = 81;
            deckController.doorDeck = new Card[numItems];

            for(int i = 0; i < numItems; i++)
            {
                var card = Resources.Load<Card>(doorPrefabsPath + "/" + i);
                deckController.doorDeck[i] = card;
            }
        }
        if (GUILayout.Button("Setup Treasure Deck"))
        {
            int numItems = 76;
            deckController.treasureDeck = new Card[numItems];

            for(int i = 0; i < numItems; i++)
            {
                var card = Resources.Load<Card>(treasurePrefabsPath + "/" + i);
                deckController.treasureDeck[i] = card;
            }
        }
    }
}
