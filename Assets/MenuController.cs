/**
* @file MenuController.cs
* @brief MenuController script file qui permet de choisir l'activité à lancer
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /// fonction qui permet de lancer l'activité CatchAndThrow
    public void PlayCatchAndThrow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// fonction qui permet de lancer l'activité ConnaissanceAnimaux
    public void PlayConnaissanceAnimaux(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }  
}
