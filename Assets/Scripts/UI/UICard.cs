using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public Card card;
    public bool isDragging = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkCardOnTable();
    }

    // See if you need to put the card on the table
    private void checkCardOnTable()
    {
        // Not dragging, no need to check
        if (!isDragging)
        {
            return;
        }

        // Check for collision with table
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20.0f);

        // Check for table
        foreach (var hit in hits)
        {
            // On the table
            if (hit.collider.tag == "table")
            {
                // Place the card
                card.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                card.transform.rotation = Quaternion.identity;

                // Enable the card for the clients
                MunchkinPlayer.instance.CmdEnableCard(card.netId);

                // Remove the card from the hand controller and the UI manager
                HandController.instance.RemoveCardFromHand(card);
                HandController.instance.handUI.RemoveCard(GetComponent<Image>());
            }
        }
    }

    public void ShowDetails()
    {
        UIController.instance.ShowCardDetails(card);
    }

    public void HideDetails()
    {
        UIController.instance.HideCardDetails();
    }

    // Mouse was clicked on card
    public void Clicked()
    {
        isDragging = true;
    }

    // Mouse was released
    public void Released()
    {
        isDragging = false;
    }

}
