/**
* @file Clicking.cs
* @brief Script permettant de gérer les boutons du menu principal
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Events;

public class Clicking : MonoBehaviour
{
    
    void Start()
    {
        
    }
    /// fonction permettant de lancer l'activité PlayCatchAndThrow
    public void PlayCatchAndThrow()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }   
    
    /// fonction permettant de lancer l'activité PlayConnaissanceAnimaux
     public void PlayConnaissanceAnimaux()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }  

    
}
