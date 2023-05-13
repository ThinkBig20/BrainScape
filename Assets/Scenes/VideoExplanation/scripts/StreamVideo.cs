using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

namespace BrainScape
{
    public class StreamVideo : MonoBehaviour
    {
        
        public VideoPlayer videoPlayer;
       
       

        void Start(){
            videoPlayer.loopPointReached += CheckOver;
        }
 
        void CheckOver(UnityEngine.Video.VideoPlayer vp)
        {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2 );
        }
        // Update is called once per frame
        void Update()
        {
           
        }
    }
}


