using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeck : MonoBehaviour
{

    public Transform placeOnTableLocation;  // Where the card will go when placed on the table

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
        MunchkinPlayer.instance.CmdDrawCard(DeckController.CardType.Door);
    }

    // Place a card from the deck on the table
    public void PlaceOnTable()
    {
        var card = DeckController.instance.DrawCard(DeckController.CardType.Door, MunchkinPlayer.instance.playerID);
        card.transform.position = placeOnTableLocation.position;
        card.transform.rotation = Quaternion.identity;
    }

}
