using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform rotatePoint;   // The point in space to rotate around

    public float moveSpeed;
    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateCamera();
        zoomCamera();
    }

    // Rotate the camera around the table
    private void rotateCamera()
    {
        float horiz = Input.GetAxis("Horizontal") * -1;
        float vert = Input.GetAxis("Vertical");

        // First rotate horizontal
        transform.RotateAround(Vector3.zero, Vector3.up, horiz * moveSpeed * Time.deltaTime);

        // Then rotate vertical
        transform.RotateAround(rotatePoint.position, transform.TransformDirection(Vector3.right), vert * moveSpeed * Time.deltaTime);
    }

    // Zoom the camera in and out from the table
    private void zoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll == 0.0f)
        {
            return;
        }

        // Get direction to move on
        Vector3 moveDirection = rotatePoint.position - transform.position;
        moveDirection.Normalize();

        // Move
        transform.position = transform.position + (moveDirection * scroll * Time.deltaTime * scrollSpeed);
    }

}
