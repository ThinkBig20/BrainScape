/**
* @file Lose.cs
* @brief Ce script permet de relancer la scène en cas de défaite
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BrainScape
{
    public class Lose : MonoBehaviour
    {
        /// la source audio attachée à l'objet 
        AudioSource audioSource;
      
       
        // Start is called before the first frame update
        void Start()
        {
           audioSource = GetComponent<AudioSource>();
        }
        

        /// la fonction PlayAudio permet de jouer le son attaché à l'objet et relancer la scène
         public void PlayAudio()
        {   
        audioSource.Play();

        StartCoroutine(waitForSound()); //Start Coroutine

        }

        /// la fonction waitForSound permet d'attendre la fin du son pour relancer la scène        
         IEnumerator waitForSound()
    {
        //Wait Until Sound has finished playing
        while (audioSource .isPlaying)
        {
            yield return null;
        }

         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
