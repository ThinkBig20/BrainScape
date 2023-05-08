/**
* @file Audio_play.cs
* @brief Audio_play script file qui permet de jouer le son du chien
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_play : MonoBehaviour
{
    /// audio source attaché au chien
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// fonction qui permet de jouer le son du chien
    public void PlayAudio()
    {
        // audioSource.Play();
        audioSource.Play();   
    }

}
