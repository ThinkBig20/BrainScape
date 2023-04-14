using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    public float catchRadius = 0.8f;
    private Rigidbody rb;
    private FixedJoint fj;

    public string StuckObject="Plane";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag(StuckObject)){
             rb.isKinematic = true;
        }
        }

    void Update()
    {
        // Check if the ball is within reach of either hand
        bool isLeftHandNear = Vector3.Distance(leftHand.transform.position, transform.position) < catchRadius;
        bool isRightHandNear = Vector3.Distance(rightHand.transform.position, transform.position) < catchRadius;

        if (isLeftHandNear || isRightHandNear)
        {
            // Disable physics simulation and attach the ball to the hand
            rb.isKinematic = true;
        }
        
    }
}
