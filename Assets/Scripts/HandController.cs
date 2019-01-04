using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public Vector2 handBounds;  // The bounds for the player's hand
    public static HandController instance;

    private List<Card> currentHand = new List<Card>(); // The current hand of the player

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add a card to your hand
    public void AddCardToHand(Card card){
        currentHand.Add(card);
    }

    // Remove the card from the hand
    // TODO: Test for duplicate cards in different hand locations
    public void RemoveCardFromHand(Card card){
        currentHand.Remove(card);
    }

    // Remake the hand so all the cards fit
    private void redrawHand(){
        
    }

}
