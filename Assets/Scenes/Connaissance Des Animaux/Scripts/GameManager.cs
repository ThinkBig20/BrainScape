/**
* @file GameManager.cs
* @brief Ce script permet de gérer le déroulement du jeu de l'activite de reconaissance des animaux
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BrainScape
{
    public class GameManager : MonoBehaviour
    {
        /**
        *objet representant l'animal dans la scene
        */
        public GameObject dogObject;
        /**
        *objet representant une canvas sur laquelle la question du quiz va s'afficher
        */
        public GameObject quizCanvas;
        /**
        *le morceau d'audio qui contient la question du quiz
        */
        public AudioClip quizVoice;
        /**
        *le morceau d'audio qui contient les instructions du quiz
        */
        public AudioClip instructionsClip;
        /**
        *le morceau d'audio qui joue le son de l'animal a decouvrir
        */
        public AudioClip dogSound;
        /**
        *le morceau d'audio contenant le nom de l'animal a decouvrir
        */
        public AudioClip dog;
        /**
        *le morceau d'audio contenant le nom de l'animal qui n'est pas a decouvrir
        */
        public AudioClip elephant;
        /**
        *objet representant la carte qui contient la bonne reponse
        */
        public GameObject correctCard;
        /**
        *objet representant la carte qui contient la mauvaise reponse
        */
        public GameObject wrongCard;
        /**
        *le composant audio source qui va jouer les morceaux d'audio
        */
        AudioSource voiceSource;
        /**
        *la fonction responsable sur le declenchement du quiz
        */
        public void ShowQuizCards()
        {
            dogObject.SetActive(false);
            voiceSource = GetComponent<AudioSource>();
            StartCoroutine(StartQuiz());
        }
        
        /**
        *une coroutine qui permet d'afficher sequenciellement les canvas et de jouer les morceaux d'audio
        */
        IEnumerator StartQuiz()
        {
            yield return new WaitForSeconds(3f);
            quizCanvas.SetActive(true);
            voiceSource.clip = quizVoice;
            voiceSource.Play();
            yield return new WaitForSeconds(voiceSource.clip.length);
            voiceSource.Stop();
            voiceSource.clip = dogSound;
            voiceSource.Play();
            yield return new WaitForSeconds(voiceSource.clip.length);
            voiceSource.Stop();
            quizCanvas.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            correctCard.SetActive(true);
            voiceSource.clip = dog;
            voiceSource.Play();
            yield return new WaitForSeconds(voiceSource.clip.length);
            voiceSource.Stop();
            wrongCard.SetActive(true);
            voiceSource.clip = elephant;
            voiceSource.Play();
            yield return new WaitForSeconds(voiceSource.clip.length);
            voiceSource.Stop();
            yield return new WaitForSeconds(0.5f);
            voiceSource.clip = instructionsClip;
            voiceSource.Play();
            yield return new WaitForSeconds(voiceSource.clip.length);
            voiceSource.Stop();
        }
        
    }
}
