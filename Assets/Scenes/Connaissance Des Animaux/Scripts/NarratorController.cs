/**
* @file NarratorController.cs
* @brief Ce script permet de gerer l'affichage de la description de l'animal et de ses differents position lors de son animation
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BrainScape
{
    public class NarratorController : MonoBehaviour
    {
        /**
        *canva qui contient le nom de l'animal
        */
        public GameObject nameDesc;
       /**
       *canva qui contient une description general sur l'animal
       */
        public GameObject generalDesc ;
        /**
        *canva qui contient une description de la position d'attack de l'animal
        */
        public GameObject attackDesc;
        /**
        *canva qui contient une description de la position de combat de l'animal
        */
        public GameObject fightDesc ;
        
        /**
        *audio contenant le son de l'animal
        */
        public AudioClip barkingClip;
        /**
        *audio contenant le nom de l'animal
        */
        public AudioClip nameClip;
        /**
        *audio contenant une description general sur l'animal
        */
        public AudioClip generalDescriptionClip;
        /**
        *audio contenant une description du son de l'animal
        */
        public AudioClip barkingDescriptionClip;
        /**
        *audio contenant une description de la position d'attack de l'animal
        */
        public AudioClip attackDescriptionClip;
        /**
        *audio contenant une description de la position de combat de l'animal
        */
        public AudioClip fightDescriptionClip;
        /**
        *objet source d'audio responsable de jouer les morceaux d'audio
        */
        AudioSource voiceOver;
        /**
        *une reference a la classe du GameManager deja creee qui permet de gerer le deroulement du jeu
        */
        GameManager gameManager;
        /**
        *composant qui permet de controller les animations
        */
        Animator animatorComponent;
        
        /**
        *fonction Start qui est invoquer par unity une fois lors du debut du script
        */
        void Start()
        {   
            gameManager = FindObjectOfType<GameManager>();
            voiceOver = GetComponent<AudioSource>();
            animatorComponent = GetComponent<Animator>();
        }
        
        /**
        *coroutine qui permet d'afficher le nom et la description general de l'animal ainsi
        *que de jouer les morceaux d'audio correspondant
        */
        IEnumerator ShowNameCoroutine(){
            nameDesc.SetActive(true);
            voiceOver.clip = nameClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            nameDesc.SetActive(false);
            yield return new WaitForSeconds(1f);
            generalDesc.SetActive(true);
            voiceOver.clip = generalDescriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            generalDesc.SetActive(false);
        }
        
        /**
        *coroutine responsable de jouer le son de l'animal
        */
        IEnumerator BarkingCoroutine(){
            voiceOver.clip = barkingClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            animatorComponent.SetBool("isBarking",false);
        }
        
        /**
        *coroutine responsable d'afficher la description du son de l'animal
        */
        IEnumerator BarkingDescription()
        {
            voiceOver.clip = barkingDescriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            animatorComponent.SetBool("isBarking",true);
        }
        

        /**
        *coroutine responsable d'afficher la description de la position d'attack de l'animal
        */
        IEnumerator ShowAttackDescription()
        {
            animatorComponent.SetBool("isAttacking",true);
            attackDesc.SetActive(true);
            voiceOver.clip = attackDescriptionClip;
            voiceOver.Play(); 
            yield return new WaitForSeconds(attackDescriptionClip.length);
            voiceOver.Stop();
            attackDesc.SetActive(false);
            animatorComponent.SetBool("isAttacking",false);
        }
        
        /**
        *coroutine responsable d'afficher la description de la position de combat de l'animal
        */
        IEnumerator ShowFightDescription()
        {
            animatorComponent.SetBool("isFighting",true);
            fightDesc.SetActive(true);
            voiceOver.clip = fightDescriptionClip;
            voiceOver.Play(); 
            yield return new WaitForSeconds(fightDescriptionClip.length);
            voiceOver.Stop();
            fightDesc.SetActive(false);
            gameManager.ShowQuizCards();
        }
    }

}

