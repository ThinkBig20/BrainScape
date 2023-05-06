using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace BrainScape
{
    public class Win : MonoBehaviour
    {
        AudioSource audioSource;
        public AudioClip[] audioClips;
        // Start is called before the first frame update
        void Start()
        {
           audioSource = GetComponent<AudioSource>();
        }
        

         public void PlayAudio()
        {
        // audioSource.Play();
        audioSource.Play();   
         foreach (AudioClip clip in audioClips)
        {
        audioSource.clip = clip;
        audioSource.Play();

        // Wait for the clip to finish playing
        while (audioSource.isPlaying)
        {
             
        }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
