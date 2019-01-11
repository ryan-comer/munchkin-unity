using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollOnMouseAtEdge : MonoBehaviour
{

    public Vector2 scrollBounds;    // Left and right bounds to start scrolling the scroll area (0.0 - 1.0)
    public float yBounds;   // The maximum y for the scroll to take effect
    public float scrollSpeed;   // How fast to scroll

    public ScrollRect scrollRect;  // The scroll rect to scroll

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkScroll();
    }

    // Check if you need to scroll the area
    private void checkScroll()
    {
        float mouseX = (float)Input.mousePosition.x / (float)Screen.width;
        float mouseY = (float)Input.mousePosition.y / (float)Screen.height;

        // Y is too high to take effect
        if(mouseY > yBounds)
        {
            return;
        }
        
        // Scroll left
        if(mouseX < scrollBounds.x)
        {
            scroll(-1);
        }
        // Scroll right
        else if(mouseX > scrollBounds.y)
        {
            scroll(1);
        }
    }

    // Scroll the scroll area
    private void scroll(int direction)
    {
        float scrollAmount = scrollSpeed * Time.deltaTime * direction;

        scrollRect.horizontalNormalizedPosition = scrollRect.horizontalNormalizedPosition + scrollAmount;
    }

}
