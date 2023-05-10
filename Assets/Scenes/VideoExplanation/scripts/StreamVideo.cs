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
        public RawImage rawImage;
        public VideoPlayer videoPlayer;
       
       

        // Start is called before the first frame update
        void Start()
        {
            
           StartCoroutine(PlayVideo());
        }
         
        IEnumerator PlayVideo()
        {
           
            videoPlayer.Prepare();
            WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
            while (!videoPlayer.isPrepared)
            {
                yield return waitForSeconds;
                break;
            }
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
           
            while(videoPlayer.isPlaying){
                yield return null;
            }
              
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
            
           
            
        }

        // Update is called once per frame
        void Update()
        {
           
        }
    }
}
