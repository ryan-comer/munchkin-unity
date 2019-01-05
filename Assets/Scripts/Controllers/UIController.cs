using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public RectTransform cardDetailsSlot;   // Where the card details will go

    public static UIController instance;

    private void Awake()
    {
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

    // User hovered over a card, show the card details visibly on the screen
    public void ShowCardDetails(Card card)
    {
        // Get the details texture for the card
        Texture2D texToShow = (Texture2D)card.frontTexture;
        Sprite spriteToShow = Sprite.Create(texToShow, new Rect(0, 0, texToShow.width, texToShow.height), new Vector2(0.5f, 0.5f), 100.0f);

        // Set the details texture
        cardDetailsSlot.GetComponent<Image>().sprite = spriteToShow;
        cardDetailsSlot.gameObject.SetActive(true); // Show the details
    }

    public void HideCardDetails()
    {
        cardDetailsSlot.gameObject.SetActive(false);
    }

    // User right clicked on the door deck
    public void DoorDeckRightClick()
    {
        // Create a context menu to choose from
    }

}
