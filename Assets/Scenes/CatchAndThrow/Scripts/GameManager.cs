using System;
using UnityEngine;
using TMPro;

namespace Scripts{

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public TextMeshProUGUI scoreText;
    public GameObject basket;
    LaunchScript firePoint;
    int missedCatchsScore = 0;
    int missedThrowsScore = 0;
    int totalScore = 0;
    int scoreThreshold = 100;
    int scoreIncrement = 20;
    void Start()
    {
        firePoint = FindObjectOfType<LaunchScript>();
        basket.SetActive(false);
    }

    public void OnMissedCatch()
    {
        firePoint.GivePermissionToLaunch();
        missedCatchsScore++;
    }

    public void OnSuccessfulCatch(){
        ShowBasket();
        // soundManager.PlayCheeringShound();
    }

    public void OnMissedThrow()
    {
        firePoint.GivePermissionToLaunch();
        missedThrowsScore++;
        HideBasket();
    }
    public void OnSuccessfulThrow()
    {
        // soundManager.PlayCheeringShound();
        totalScore+=scoreIncrement;
        scoreText.text = totalScore.ToString();
        Invoke("HideBasket",2f);
        if(totalScore<scoreThreshold)
        {
            firePoint.GivePermissionToLaunch();
        }
        else
        {
            WonLevel();
        }
    }

    public void ShowBasket(){
        basket.SetActive(true);
    }
    public void HideBasket(){
        basket.SetActive(false);
    }

    void WonLevel()
    {

    }
}
}
