using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrainScape
{
    public class Win : MonoBehaviour
    {
        AudioSource audioSource;
        // Start is called before the first frame update
        void Start()
        {
           audioSource = GetComponent<AudioSource>();
        }
        

         public void PlayAudio()
        {
        // audioSource.Play();
        audioSource.Play();   

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
