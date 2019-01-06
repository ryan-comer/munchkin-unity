using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickable : MonoBehaviour
{

    public Canvas canvas;
    public RectTransform contextMenu_p;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkRightClick();
        checkContextMenuClear();
    }

    // See if the user right clicked this object
    private void checkRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Use a ray to see if the object was right clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    var contextMenu = Instantiate(contextMenu_p, Input.mousePosition, Quaternion.identity);
                    contextMenu.GetComponent<ContextMenu>().target = gameObject;
                    contextMenu.SetParent(canvas.transform);
                }
            }
        }
    }

    // Clear the context menu if the cursor is not over it
    private void checkContextMenuClear()
    {

    }

}
