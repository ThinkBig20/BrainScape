using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

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
         StartCoroutine(waitForSound());

        }
         IEnumerator waitForSound()
        {
        //Wait Until Sound has finished playing
        while (audioSource .isPlaying)
        {
            yield return null;
        }
       //Auidio has finished playing, disable GameObject
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
