using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public void PlayCatchAndThrow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayConnaissanceAnimaux(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }  
}
