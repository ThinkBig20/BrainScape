using UnityEngine;
using TMPro;

namespace Scripts{

public class LaunchScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform playerPosition;
    public TextMeshProUGUI timerUI;
    float fireInterval = 10f;
    float currentTime = 0f;
    public float launchVelocity = 2610f;
    bool launchBall = true;
    SoundManager soundManager;
    
    // Start is called before the first frame update
    

    void Start()
    {
        currentTime = fireInterval;
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
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
    
    public void Launch()
    {
        launchBall = false;
        GameObject projectileClone = Instantiate(projectile, transform.position, transform.rotation);
        projectileClone.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,launchVelocity,0));
        // soundManager.PlayShootingSound();
    }

    public void GivePermissionToLaunch()
    {
        launchBall = true;
    }

}
}
