using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DiscardPile : NetworkBehaviour
{
    public GameObject topCardImage;  // The top card of the discard pile
    public GameObject deck;

    public DeckController.CardType cardType;    // The card type for this discard pile

    private List<Card> currentCards = new List<Card>(); // The cards currently in the discard pile

    // The ammount of cards in the pile
    [SyncVar]
    private int currentCardCount;

    // Start is called before the first frame update
    void Start()
    {
        Material mat = new Material(Shader.Find("Legacy Shaders/Diffuse"));
        topCardImage.GetComponent<MeshRenderer>().material = mat;

        initializeSize();

        // Hide at the beginning
        hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void hide()
    {
        topCardImage.SetActive(false);
        deck.GetComponent<MeshRenderer>().enabled = false;
    }

    private void show()
    {
        topCardImage.SetActive(true);
        deck.GetComponent<MeshRenderer>().enabled = true;
    }

    // Add a card to this discard pile (Server)
    public void AddCard(Card card)
    {
        if (!isServer)
        {
            return;
        }

        currentCards.Insert(0, card);
        currentCardCount = currentCards.Count;

        RpcAddCard(card.gameObject);
    }

    // Client function for adding a card
    [ClientRpc]
    public void RpcAddCard(GameObject card)
    {
        show();

        // Update the physical size
        updatePhysicalSize();

        // Update the front image
        updateTopCardImage(card);
    }

    // Update the size of the discard pile
    private void updatePhysicalSize()
    {

        float newY = 0.0f;
        switch (cardType)
        {
            case DeckController.CardType.Door:
                newY = (float)currentCardCount / (float)DeckController.instance.GetMaxCards(DeckController.CardType.Door);

                break;
            case DeckController.CardType.Treasure:
                newY = (float)currentCardCount / (float)DeckController.instance.GetMaxCards(DeckController.CardType.Treasure);

                break;
        }

        transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
    }

    // Start off empty
    private void initializeSize()
    {
        float newY = 0.0f;

        transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
    }

    // Change the image of the top card based on the current cards
    private void updateTopCardImage(GameObject card)
    {
        Card topCard = card.GetComponent<Card>();
        topCardImage.GetComponent<MeshRenderer>().material.mainTexture = topCard.frontTexture;
    }

}
