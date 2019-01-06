using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Vector3 cardRotationOffset;

    public float handDistanceFromCamera;    // How far away the hand is from the camera
    [Range(0, 1)]
    public float handDistanceFromBottom;    // How far away the hand is from the bottom of the viewport

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
        redrawHand();
    }

    // Remove the card from the hand
    // TODO: Test for duplicate cards in different hand locations
    public void RemoveCardFromHand(Card card){
        card.isInHand = false;
        currentHand.Remove(card);
        redrawHand();
    }

    // Remake the hand so all the cards fit
    private void redrawHand(){
        // Get the coordinates for the snap points
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, handDistanceFromBottom, handDistanceFromCamera));
        Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, handDistanceFromBottom, handDistanceFromCamera));

        int numCards = currentHand.Count;   // Use number of cards to get the separation
        Vector3 direction = bottomRight - bottomLeft;
        float xRange = direction.magnitude;
        float xInterval = xRange / (numCards + 1);

        // Loop through and place cards
        for(int i = 0; i < numCards; i++)
        {
            Vector3 newPosition = bottomLeft;
            newPosition = newPosition + (direction.normalized * xInterval*(i+1));
            currentHand[i].transform.position = newPosition;
            currentHand[i].transform.rotation = Camera.main.transform.rotation;
            currentHand[i].transform.Rotate(cardRotationOffset);
        }
    }

}
