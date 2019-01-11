using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public HandUI handUI;   // The UI controller for the hand UI

    public static HandController instance;

    [SerializeField]
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
        card.isInHand = true;
        currentHand.Add(card);

        handUI.AddCard(card);   // Add to the UI handler for the hand
    }

    // Remove the card from the hand
    public void RemoveCardFromHand(Card card){
        card.isInHand = false;
        currentHand.Remove(card);
    }

}
