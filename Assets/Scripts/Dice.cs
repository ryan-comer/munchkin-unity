using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    public float heightWhenDragged;
    public float dragDamper;    // Controls the speed of the drag
    public float velocityMultiplier;
    public float angularVelocity;
    
    private bool beingDragged;
    private Rigidbody rigid;
    private Plane movePlane;    // The plane that the dice can move on
    private Vector3 moveTo; // The vector to move to

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.maxAngularVelocity = angularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        checkMouseClick();
        updateMoveToVector();
    }

    // Physics update
    void FixedUpdate()
    {
        moveDice();
        rollDice();
    }

    // See if the user clicked the dice
    private void checkMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.root.transform == transform)
                {
                    movePlane = new Plane(Vector3.up, transform.position + Vector3.up * heightWhenDragged);

                    rigid.useGravity = false;
                    beingDragged = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && beingDragged)
        {
            beingDragged = false;
            rigid.useGravity = true;
        }
    }

    // Update the position the dice has to move to
    private void updateMoveToVector()
    {
        if (beingDragged)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;

            var hasHit = movePlane.Raycast(ray, out dist);
            if (hasHit)
            {
                moveTo = ray.GetPoint(dist);
            }
        }
    }

    // Move the dice around with the mouse if necessary
    private void moveDice()
    {
        if (!beingDragged)
        {
            return;
        }

        var velocity = moveTo - transform.position;
        velocity *= velocityMultiplier;
        rigid.velocity = Vector3.Lerp(rigid.velocity, velocity, dragDamper);
    }

    private void rollDice()
    {
        if (!beingDragged)
        {
            return;
        }

        rigid.angularVelocity = new Vector3(angularVelocity, angularVelocity, angularVelocity);
    }

}
