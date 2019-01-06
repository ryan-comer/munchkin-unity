using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    public GameObject topCardImage;  // The top card of the discard pile
    public GameObject deck;

    private List<Card> currentCards = new List<Card>(); // The cards currently in the discard pile

    // Start is called before the first frame update
    void Start()
    {
        Material mat = new Material(Shader.Find("Legacy Shaders/Diffuse"));
        topCardImage.GetComponent<MeshRenderer>().material = mat;

        updatePhysicalSize();

        topCardImage.SetActive(false);
        deck.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add a card to this discard pile
    public void AddCard(Card card)
    {
        currentCards.Insert(0, card);
        updatePhysicalSize();

        // Make visible
        topCardImage.SetActive(true);
        deck.GetComponent<MeshRenderer>().enabled = true;

        updateTopCardImage();
    }

    // Update the size of the discard pile
    private void updatePhysicalSize()
    {
        float newY = (float)currentCards.Count / (float)DeckController.instance.GetMaxCards(DeckController.CardType.Door);
        transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
    }

    // Change the image of the top card based on the current cards
    private void updateTopCardImage()
    {
        // Empty
        if(currentCards.Count == 0)
        {
            return;
        }

        Card topCard = currentCards[0];
        topCardImage.GetComponent<MeshRenderer>().material.mainTexture = topCard.frontTexture;
    }

}
