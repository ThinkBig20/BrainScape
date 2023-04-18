using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public AudioClip backgroundClip;
        public AudioClip shootSound;
        public AudioClip cheering;
        private AudioSource source;
        // Start is called before the first frame update
        void Start()
        {
            source = GetComponent<AudioSource>();
        }

        public void PlayBackgroundSound(){
            source.clip = backgroundClip;
            source.Play();
        }

        public void PlayShootingSound(){
            source.PlayOneShot(shootSound);
        }

        public void PlayCheeringShound(){
            source.PlayOneShot(cheering);
        }
    }
}
