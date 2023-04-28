using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrainScape
{
    public class AnimationScript : MonoBehaviour
    {
        public float mouvementSpeed = 20f;
        public float rotationSpeed = 50f;

        private bool isWandering = false;
        private bool isRotatingLeft = false;
        private bool isRotatingRight = false;
        private bool isWalking = false;

        Rigidbody rb;
        Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!isWandering){
                StartCoroutine(Wander());
            }

            if(isRotatingLeft){
                transform.Rotate(transform.up * -rotationSpeed * Time.deltaTime);
            }
            if(isRotatingRight){
                transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
            }
            if(isWalking){
                rb.AddForce(transform.forward * mouvementSpeed);
                animator.SetBool("isRunning",true);
            }
            if(!isWalking){
                animator.SetBool("isRunning",false);
            }
        }

        IEnumerator Wander(){
            int rotationTime = Random.Range(1, 3);
            int rotateWait = Random.Range(1, 3);
            int rotationDirection = Random.Range(1, 2);

            int walkTime = Random.Range(1, 3);
            int walkWait = Random.Range(1, 3);

            isWandering = true;

            yield return new WaitForSeconds(walkWait);

            isWalking = true;

            yield return new WaitForSeconds(walkTime);

            isWalking = false;

            yield return new WaitForSeconds(rotateWait);

            if(rotationDirection == 1){
                isRotatingLeft = true;
                yield return new WaitForSeconds(rotationTime);
                isRotatingLeft = false;
            }
            if(rotationDirection == 2){
                isRotatingRight = true;
                yield return new WaitForSeconds(rotationTime);
                isRotatingRight = false;
            }
            isWandering = false;
        }
    }
}
