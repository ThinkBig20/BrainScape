/**
* @file Lose.cs
* @brief Ce script permet de relancer la scène en cas de défaite
* @version 1.0
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
        

        /// la fonction PlayAudio permet de jouer le son attaché à l'objet
         public void PlayAudio()
        {   
        audioSource.Play();

        StartCoroutine(waitForSound()); //Start Coroutine

      
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
