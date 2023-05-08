using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrainScape
{
    public class BallLine : MonoBehaviour
    {
        public LineRenderer lineRenderer;

        // Start is called before the first frame update
        void Start()
        {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        }
        // Update is called once per frame
        void Update()
        {
        Vector3 launchDirection = (-transform.forward);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + launchDirection * 10f); 
        // 5f is the length of the line
        }
    }
}

