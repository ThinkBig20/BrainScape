using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Events;

public class Clicking : MonoBehaviour
{
    [SerializeField]
     public GameObject button;
     public UnityEvent OnPress;
     public UnityEvent OnRelease;
     private bool isPressed = false;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            OnPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            OnRelease.Invoke();
              
            isPressed = false;
        }
    }

    public void PlayCatchAndThrow()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }   

}
