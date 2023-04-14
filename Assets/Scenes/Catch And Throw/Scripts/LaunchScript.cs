using UnityEngine;
using UnityEngine.UI;

public class LaunchScript : MonoBehaviour
{
    public GameObject projectile;
    // public float fireAngle = 45.0f;
    // public float gravity = 9.8f;
    public Transform playerPosition;
    public Text timerUI;
    float fireInterval = 10f;
    float currentTime = 0f;
    public float launchVelocity = 2610f;
    
    // Start is called before the first frame update
    

    void Start()
    {
        currentTime = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {
         currentTime-=1*Time.deltaTime;
        timerUI.text = currentTime.ToString("0");
        if(currentTime<=0)
        {
            Launch();
            currentTime = fireInterval;
        }
    }
    
    public void Launch()
    {
        GameObject projectileClone = Instantiate(projectile, transform.position, transform.rotation);
        projectileClone.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,launchVelocity,0));
        
        // calculate the direction vector from the cannon to the player
    //    Vector3 targetDirection = playerPosition.position - transform.position;

    //     // calculate the distance to the player
    //     float distanceToPlayer = targetDirection.magnitude;

    //     // calculate the velocity needed to reach the player
    //     float projectileVelocity = distanceToPlayer / (Mathf.Sin(2 * fireAngle * Mathf.Deg2Rad) / gravity);

    //     // calculate the x and y velocities
    //     float vx = projectileVelocity * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
    //     float vy = projectileVelocity * Mathf.Sin(fireAngle * Mathf.Deg2Rad);

    //     // set the initial velocity of the cannonball
    //     Rigidbody cannonBallRigidbody = projectileClone.GetComponent<Rigidbody>();
    //     cannonBallRigidbody.AddForce((transform.forward * vx + transform.up * vy)*launchVelocity, ForceMode.VelocityChange);

    }
}
