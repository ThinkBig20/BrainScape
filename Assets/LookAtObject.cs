using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrainScape
{
    public class LookAtObject : MonoBehaviour
    {
        [SerializeField] 
        GameObject objectToLookAt;
        // Start is called before the first frame update
        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {
           transform.rotation = Quaternion.LookRotation(transform.position - objectToLookAt.transform.position);
        }
    }
}
