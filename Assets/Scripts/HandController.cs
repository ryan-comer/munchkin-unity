using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject handPlatform;
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
        currentHand.Add(card);
        redrawHand();
    }

    // Remove the card from the hand
    // TODO: Test for duplicate cards in different hand locations
    public void RemoveCardFromHand(Card card){
        currentHand.Remove(card);
        redrawHand();
    }

    // Remake the hand so all the cards fit
    private void redrawHand(){
        
    }

}
