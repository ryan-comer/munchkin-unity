using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public Texture frontTexture;
    public int playerOwner = -1; // The player that owns this card

    // Start is called before the first frame update
    void Start()
    {
        getFrontImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get the front image for the card
    // Based on naming convention
    private void getFrontImage()
    {

    }

}
