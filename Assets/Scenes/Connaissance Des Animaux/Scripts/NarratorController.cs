using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrainScape
{
    public class NarratorController : MonoBehaviour
    {
        public GameObject descriptionCanva;
        public GameObject nameCanva;
        public AudioClip nameClip;
        public AudioClip descriptionClip;
        AudioSource voiceOver;
        
        void Start()
        {   
            voiceOver = GetComponent<AudioSource>();
            StartCoroutine(StartNarrator());
        }

        IEnumerator StartNarrator()
        {
            yield return new WaitForSeconds(5f);
            nameCanva.SetActive(true);
            yield return new WaitForSeconds(2f);
            voiceOver.clip = nameClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            nameCanva.SetActive(false);

            yield return new WaitForSeconds(5f);

            descriptionCanva.SetActive(true);
            yield return new WaitForSeconds(2f);
            voiceOver.clip = descriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            yield return new WaitForSeconds(5f);
            descriptionCanva.SetActive(false);
        }

    }

}

