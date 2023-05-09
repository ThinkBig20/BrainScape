/**
* @file GameManager.cs
* ce script contient les différentes fonctions qui permettent de gérer les événements de l'activite CatchAndThrow
* que ça soit l'affichage du score, la gestion de l'affichage du basket cible, la gestion d'un lancement réussie ou échoue
* quand la ball va être relancer et quand non, il gérer les différentes actions de l'activite
*/

using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Scripts{

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    AudioSource audioSource;
    /// score du jeu
    public TextMeshProUGUI scoreText;
    /// le component basket cible où l'utilisateur va lancer la ball vers
    public GameObject basket;
    /// le canon ou bien l objet qui va lancer la ball vers l'utilisateur
    LaunchScript firePoint;
    /// score des essais échouent (de saisir ou bien prendre avec les mains la ball ) 
    int missedCatchsScore = 0;
    /// score des  essais échouent (de lancement du ball)
    int missedThrowsScore = 0;
    /// le score total des essais réussis (du lancement du ball au basket cible)
    int totalScore = 0;
    /// le score max du fin du jeu
    int scoreThreshold = 100;
    /// l'increment avec lequelle le score totale va augumenter
    int scoreIncrement = 20;
    private bool hasScored=false;
    public AudioClip[] audioclips;

    ///  la fonction genere par unity , permet ici d initialiser le firePoint objet et mettre la basket invisible 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        firePoint = FindObjectOfType<LaunchScript>();
        basket.SetActive(false);
    }

    /// Cette fonction gère les  essais échouent à prendre la balle, elle donne l'ordre au fiRepoint de relancer une autre ball et incrémente la variable missedcatchsscore ( le nombre des  essais échoué de prendre la balle)
    public void OnMissedCatch()
    {
        firePoint.GivePermissionToLaunch();
        missedCatchsScore++;
        audioSource.clip = audioclips[0];
        audioSource.Play();
    }

    /// Cette fonction gère l'essaie réussie à prendre la ball par l'utilisateur, à chacun essaie réussie elle affiche la basket cible
    public void OnSuccessfulCatch(){
        ShowBasket();
        audioSource.clip = audioclips[2];
        audioSource.Play();
        // soundManager.PlayCheeringShound();
    }

    /// Cette fonction gere l'essaie échoué de l'utilisateur à lancer la ball vers la basket cible, il a raté l'objet cible, elle permet de donner l'ordre au fiRepoint a relancé la ball, incrémenter le missedthrowsscore et a caché la basket
    public void OnMissedThrow()
    {   
        audioSource.clip = audioclips[0];
        audioSource.Play();
        firePoint.GivePermissionToLaunch();
        missedThrowsScore++;
        HideBasket();
    }

    IEnumerator ResetHasScored()
    {
        yield return new WaitForSeconds(2f); // wait for 2 seconds
        hasScored = false; // reset hasScored to false
    }

    ///  cette fonction responsable sur l'essai réussie, c'est-à-dire l'utilisateur à bien lancer la ball vers la basket cible, elle incrémente le score totale avec la valeur qu'on a spécifie dans la variable scoreIncrement , et affiche ce score dans le scoretext canvas, la basket va aussi être caché, et un relancement du ball si le score n'est pas encore arrivé au scoreThreshold si non la fin du jeu
    public void OnSuccessfulThrow()
    {
        if (!hasScored)
        {
            totalScore += scoreIncrement;
            scoreText.text = totalScore.ToString();
            Invoke("HideBasket", 2f);
            if (totalScore < scoreThreshold)
            {
                firePoint.GivePermissionToLaunch();
            }
            else
            {
                WonLevel();
            }
            hasScored = true;
            StartCoroutine(ResetHasScored()); // reset hasScored after 2 seconds
        }
    }
     
    /// cette fonction responsable sur mettre la basket visible
    public void ShowBasket(){
        basket.SetActive(true);
    }
    ///  cette fonction responsable sur mettre la basket invisible
    public void HideBasket(){
        basket.SetActive(false);
    }

    void WonLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
}
