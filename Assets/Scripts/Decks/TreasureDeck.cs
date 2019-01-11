using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureDeck : MonoBehaviour
{

    public Transform placeOnTableLocation;  // Where new cards will go

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Draw a card and put in the player's hand
    public void DrawCard()
    {
        MunchkinPlayer.instance.CmdDrawCard(DeckController.CardType.Treasure);
    }

    // Place a card from the deck on the table
    public void PlaceOnTable()
    {
        var card = DeckController.instance.DrawCard(DeckController.CardType.Treasure, MunchkinPlayer.instance.playerID);
        card.transform.position = placeOnTableLocation.position;
        card.transform.rotation = Quaternion.identity;
    }

}
