using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Card : NetworkBehaviour
{

    public Texture frontTexture;

    public int playerOwner = -1; // The player that owns this card

    public DeckController.CardType cardType;    // The type of this card

    public int numInDeck = 1;
    public bool isInHand;   // Is the card in your hand
    public bool isInDiscard;    // Is this card in the discard pile

    private bool isDragging;
    private bool isFaceDown;

    // Start is called before the first frame update
    void Start()
    {
        // Start off disabled
        disable();
    }

    // Update is called once per frame
    void Update()
    {
        updateCardPosition();
    }

    private void updateCardPosition()
    {
        // Not dragging card
        if (!isDragging)
        {
            return;
        }

        // Check for collision with table
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20.0f);
        bool hitTable = false;

        // Check for table
        foreach(var hit in hits)
        {
            // On the table
            if (hit.collider.tag == "table")
            {
                transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                transform.rotation = Quaternion.identity;

                if (isInHand)
                {
                    HandController.instance.RemoveCardFromHand(this);
                }

                hitTable = true;
                break;
            }
        }

        // If you didn't hit the table, add the card back to the hand
        if(!hitTable && !isInHand)
        {
            HandController.instance.AddCardToHand(this);
        }
    }

    // Enable the card - show the card
    [ClientRpc]
    public void RpcEnableCard()
    {
        GetComponent<RightClickable>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;

        // Enable all mesh renderers
        foreach(MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = true;
        }
    }

    // Disable the card - hide the card
    [ClientRpc]
    public void RpcDisableCard()
    {
        GetComponent<RightClickable>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        // Disable all mesh renderers
        foreach(MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = false;
        }
    }

    // Set the border colors for the card
    [ClientRpc]
    public void RpcSetBorderColor(int materialIndex)
    {
        Transform border = transform.Find("border");
        for(int i = 0; i < 4; i++)
        {
            border.GetChild(i).GetComponent<MeshRenderer>().material = DeckController.instance.playerMaterials[materialIndex];
        }
    }

    // Set the player authority for this object
    public void SetPlayerAuthority(NetworkConnection conn)
    {
        NetworkIdentity netId = GetComponent<NetworkIdentity>();
        GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        Debug.Log(netId.clientAuthorityOwner);
    }

    // Place the card in the discard pile
    public void Discard()
    {
        if (isInHand)
        {
            HandController.instance.RemoveCardFromHand(this);
        }

        UIController.instance.HideCardDetails();
        //DeckController.instance.DiscardCard(this);
    }

    // Return this card to its deck
    public void ReturnToDeck()
    {
        if (isInHand)
        {
            HandController.instance.RemoveCardFromHand(this);
        }
        DeckController.instance.ReturnCard(this);
    }

    // Local function to disable the card
    private void disable()
    {
        GetComponent<RightClickable>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        // Disable all mesh renderers
        foreach (MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = false;
        }
    }

    // Card was clicked
    void OnMouseDown()
    {
        if (playerOwner == MunchkinPlayer.instance.playerID)
        {
            isDragging = true;
        }
    }

    // Card was released
    void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseEnter()
    {
        if (!isFaceDown && !isDragging)
        {
            UIController.instance.ShowCardDetails(this);
        }
    }

    private void OnMouseExit()
    {
        UIController.instance.HideCardDetails();
    }

}
