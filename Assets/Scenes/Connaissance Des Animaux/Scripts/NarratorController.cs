using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BrainScape
{
    public class NarratorController : MonoBehaviour
    {
        private string nameDesc = "ﺐﻠﻛ ﺍﺬﻫ";
        private string generalDesc = "ْﻝﺎﻔﻃﻷﺍ ﻯﺪﻟ ِﺓَﺑَّﺒﺤﻤﻟﺍ ﺔﻔﻴﻟﻷﺍ ﺕﺎﻧﺍﻮﻴﺤﻟﺍ ﻦﻣ ﺐﻠﻜﻟﺍ";
        private string barkingDesc = "ِﺡﺎﺑُّﻨﻟﺍ ُﺓَّﻳِﻋْﺿَﻭ ِﻩِﺫَﻫ";
        private string attackDesc="ِﻡﻭُﺟُﻬﻟﺍ ُﺓَّﻳِﻋْﺿَﻭ ِﻩِﺫَﻫ";
        private string fightDesc = "ِﻙﺍﺭِﻌﻟﺍ ُﺓَّﻳِﻋْﺿَﻭ ِﻩِﺫَﻫ";
        

        public TextMeshPro descriptionCanva;
        public AudioClip nameClip;
        public AudioClip generalDescriptionClip;
        public AudioClip barkingDescriptionClip;
        public AudioClip attackDescriptionClip;
        public AudioClip fightDescriptionClip;
        AudioSource voiceOver;
        GameManager gameManager;
        
        void Start()
        {   
            descriptionCanva.isRightToLeftText = true;
            gameManager = FindObjectOfType<GameManager>();
            voiceOver = GetComponent<AudioSource>();
            StartCoroutine(ShowNameCoroutine());
        }

        IEnumerator ShowNameCoroutine(){
            yield return new WaitForSeconds(2.5f);
            descriptionCanva.text = nameDesc;
            yield return new WaitForSeconds(1f);
            voiceOver.clip = nameClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForSeconds(1f);
            descriptionCanva.text = generalDesc;
            voiceOver.clip = generalDescriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();
        }


        IEnumerator ShowBarkingDescriptionCoroutine()
        {
            descriptionCanva.text = barkingDesc;
            voiceOver.clip = barkingDescriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();  
        }
        
        IEnumerator ShowAttackDescriptionCoroutine()
        {
            descriptionCanva.text = attackDesc;
            voiceOver.clip = attackDescriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();  
        }

        IEnumerator ShowFightDescriptionCoroutine()
        {
            descriptionCanva.text = fightDesc;
            voiceOver.clip = fightDescriptionClip;
            voiceOver.Play();
            yield return new WaitForSeconds(voiceOver.clip.length);
            voiceOver.Stop();  
        }

        // IEnumerator StartNarrator()
        // {
        //     yield return new WaitForSeconds(2.5f);
        //     nameCanva.SetActive(true);
        //     yield return new WaitForSeconds(1f);
        //     voiceOver.clip = nameClip;
        //     voiceOver.Play();
        //     yield return new WaitForSeconds(voiceOver.clip.length);
        //     voiceOver.Stop();
        //     nameCanva.SetActive(false);

        //     yield return new WaitForSeconds(2.5f);

        //     descriptionCanva.SetActive(true);
        //     yield return new WaitForSeconds(1f);
        //     voiceOver.clip = descriptionClip;
        //     voiceOver.Play();
        //     yield return new WaitForSeconds(voiceOver.clip.length);
        //     voiceOver.Stop();
        //     yield return new WaitForSeconds(2.5f);
        //     descriptionCanva.SetActive(false);
        //     yield return new WaitForSeconds(5f);
        //     gameManager.ShowQuizCards();
        // }

    }

}

