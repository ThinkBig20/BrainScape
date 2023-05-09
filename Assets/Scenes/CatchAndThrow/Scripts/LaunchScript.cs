/**
* @file LaunchScript.cs
* ce script est associe au canon qui va lancer la balle il permer de
* lancer la balle chaque 10s et en respectant si la balle est pris par 
* l utilisateur ou non  
* @version 1.0
*/


using UnityEngine;
using TMPro;

namespace Scripts{

public class LaunchScript : MonoBehaviour
{
    /// le projectile qui lance la balle
    public GameObject projectile;
    /// le timer qui affiche le timing à l utilisateur
    public TextMeshProUGUI timerUI;
    /// variable contient le nombre de seconds pour lancer la balle par le projectile
    float fireInterval = 10f;
    /// variable pour definir l heure actuelle 
    float currentTime = 0f;
    /// la velocity avec laquelle la balle va etre lance 
    public float launchVelocity = 2610f;
    /// une variable qui determine est ce que la balle est lance ou non
    public bool launchBall = true;

    SoundManager soundManager;
    
    // Start is called before the first frame update
    /// cette fonction permet de definir l heure actuelle  elle est appelee une fois au debut c est une fonction de unity
    void Start()
    {
        currentTime = fireInterval;
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    /// cette fonction permet de lancer la balle chaque 10s et en respectant si la balle est pris par l utilisateur ou non , c est une fonction de unity
    void Update()
    {
        if(launchBall){  
            currentTime-=1*Time.deltaTime;
            timerUI.text = currentTime.ToString("0");
            if(currentTime<=0)
            {
                Launch();
                currentTime = fireInterval;
            }
        }else{
            timerUI.text = "";
        }
    }
    
    /// cette fonction permet de lancer la balle 
    public void Launch()
    {
        launchBall = false;
        GameObject projectileClone = Instantiate(projectile, transform.position, transform.rotation);
        Rigidbody projectileRigid = projectileClone.GetComponent<Rigidbody>();
        projectileRigid.isKinematic = false;

        projectileRigid.AddRelativeForce(new Vector3(0,launchVelocity,0));
    }

    /// cette fonction  permet de donner la permission de lancer la balle
    public void GivePermissionToLaunch()
    {
        launchBall = true;
    }

}
}
