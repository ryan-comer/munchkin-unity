using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchWidthofViewport : MonoBehaviour
{

    public Camera cam;  // Camera to match the viewport of

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateWidth();
    }

    // Match the width of the viewport
    private void updateWidth(){
        // Get current distance from camera
        float distanceToCamera = Vector3.Distance(cam.transform.position, transform.position);

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, distanceToCamera));

        transform.localScale = new Vector3(topRight.x - bottomLeft.x, transform.localScale.y, transform.localScale.z);
    }

}
