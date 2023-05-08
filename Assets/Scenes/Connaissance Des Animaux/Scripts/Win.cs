/**
* @file Win.cs
* @brief Ce script permet de se placer dans la scène du choix d'activité(Start Menu) en cas de victoire
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace BrainScape
{
    public class Win : MonoBehaviour
    {
        /// la source audio attachée à l'objet
        AudioSource audioSource;
      
        // Start is called before the first frame update
        void Start()
        {
           audioSource = GetComponent<AudioSource>();
        }
        
        /// la fonction PlayAudio permet de jouer le son attaché à l'objet 
         public void PlayAudio()
        {
       
         audioSource.Play();
         StartCoroutine(waitForSound());

        }

        /// la fonction waitForSound permet d'attendre la fin du son et se deplacer vers la scène du choix d'activité(Start Menu)
         IEnumerator waitForSound()
        {
        //Wait Until Sound has finished playing
        while (audioSource .isPlaying)
        {
            yield return null;
        }
       
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
