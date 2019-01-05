using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public Texture frontTexture;
    public int playerOwner = -1; // The player that owns this card

    public int numInDeck = 1;
    public bool isInHand;   //is the card in your hand

    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {

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

    // Card was clicked
    void OnMouseDown()
    {
        isDragging = true;
    }

    // Card was released
    private void OnMouseUp()
    {
        isDragging = false;
    }

}
