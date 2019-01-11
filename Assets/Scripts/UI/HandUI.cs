using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{

    public int minCardDistance; // The minimum distance that cards can be (number of pixels)
    public Image cardUI_p;  // The image that will hold the card texture

    private Image[] cardImagesPool = new Image[40];

    private Dictionary<Card, Image> cardImageDictionary = new Dictionary<Card, Image>();    // Dictionary to keep track of which cards have which UI image in your hand

    // Start is called before the first frame update
    void Start()
    {
        initializeImagePool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add the card to the hand
    public void AddCard(Card card)
    {
        Image image = getNextAvailableImage();

        // Create the sprite
        Sprite sprite = Sprite.Create((Texture2D)card.frontTexture, new Rect(0, 0, card.frontTexture.width, card.frontTexture.height), new Vector2(0.5f, 0.5f), 100);
        image.sprite = sprite;
        image.GetComponent<UICard>().card = card;
        image.GetComponent<UICard>().isDragging = false;

        image.gameObject.SetActive(true);

        // Add to the dictionary
        cardImageDictionary[card] = image;
    }

    // Remove the card from the hand
    public void RemoveCard(Card card)
    {
        Image image = cardImageDictionary[card];
        image.gameObject.SetActive(false);
    }

    // Are the cards too close together
    private bool isTooClose()
    {
        // Not enough children to matter - need more than 1
        if(transform.childCount < 2)
        {
            return false;
        }

        // Check the distance between the first 2 cards
        var firstChild = transform.GetChild(0).GetComponent<Image>();
        var secondChild = transform.GetChild(1).GetComponent<Image>();

        float difference = (secondChild.rectTransform.position - firstChild.rectTransform.position).magnitude;

        if(difference < minCardDistance)
        {
            return true;
        }

        return false;
    }

    // Create objects for the image pool
    private void initializeImagePool()
    {
        for(int i = 0; i < cardImagesPool.Length; i++)
        {
            var obj = Instantiate(cardUI_p, transform);
            obj.preserveAspect = true;
            obj.gameObject.SetActive(false);

            obj.transform.SetParent(transform);

            cardImagesPool[i] = obj;
        }
    }

    // Scan the pool for the next available image
    private Image getNextAvailableImage()
    {
        foreach(Image image in cardImagesPool)
        {
            if (!image.gameObject.activeInHierarchy)
            {
                return image;
            }
        }

        return null;
    }

}
