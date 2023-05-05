using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BrainScape
{
    public class GameManager : MonoBehaviour
    {
        public GameObject dogObject;
        public GameObject descriptionCanvas;
        public GameObject quizCanvas;
        public AudioClip quizVoice;
        public AudioClip dogSound;
        public AudioClip dog;
        public AudioClip elephant;
        public GameObject correctCard;
        public GameObject wrongCard;
        AudioSource voiceSource;
        public void ShowQuizCards()
        {
            dogObject.SetActive(false);
            descriptionCanvas.SetActive(false);
            voiceSource = GetComponent<AudioSource>();
            StartCoroutine(StartQuiz());
        }
        
        IEnumerator StartQuiz()
        {
            yield return new WaitForSeconds(1f);
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
        }
        
    }
}
