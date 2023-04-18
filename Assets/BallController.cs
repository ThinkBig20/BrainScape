

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
public class BallController : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    public float catchRadius = 0.8f;
    private Rigidbody rb;
    private bool touched;
    

    public string StuckObject="Wall";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        touched=false;
    }

    void OnTriggerEnter(Collider   collision)
        {
        if (collision.gameObject.CompareTag("Wall") )
        {
           rb.isKinematic = true;
           Debug.Log("Ball Stuck");
        }
        }

    void Update()
    {
        // Check if the ball is within reach of either hand
        bool isLeftHandNear = Vector3.Distance(leftHand.transform.position, transform.position) < catchRadius;
        bool isRightHandNear = Vector3.Distance(rightHand.transform.position, transform.position) < catchRadius;

        if (isLeftHandNear || isRightHandNear)
        {   
            if(touched==false){
                touched=true;
                rb.isKinematic = true;
            } 
            // Enable physics simulation and detach the ball from the hand
            rb.isKinematic = false;
            // Disable physics simulation and attach the ball to the hand
        }
        
    }
    private void HandlePointerEventRaised(PointerEvent evt)
        {
            switch (evt.Type)
            {
                case PointerEventType.Select:
                        rb.isKinematic = true;

                    break;
                case PointerEventType.Unselect:
                        rb.isKinematic = false;
                        Vector3 throwDirection = transform.forward;
                        float throwForce = 10f; // Changer la valeur de la force de lancer selon votre besoin
                        // add a force to the rigidbody in the direction of the throw
                        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
                    break;
            }
        }
}
}