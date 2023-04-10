using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{ 
    public GameObject cannonBall; 
    public float fireAngle = 45.0f; 
    public float gravity = 9.8f;
    float fireInterval = 10f;
    float currentTime = 0f;
    public Transform playerPosition;  
    public Text timerUI; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireCannonBall",fireInterval,fireInterval);
        currentTime = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime-=1*Time.deltaTime;
        timerUI.text = currentTime.ToString("0");
        if(currentTime<=0)
        {
            currentTime = 0;
            timerUI.text = "";
        }
    }
    
    void FireCannonBall() {
    // instantiate a new cannonball at the cannon's position
    GameObject newCannonBall = Instantiate(cannonBall, transform.position, Quaternion.identity);
    
    // calculate the direction vector from the cannon to the player
    Vector3 targetDirection = playerPosition.position - transform.position;

    // calculate the distance to the player
    float distanceToPlayer = targetDirection.magnitude;

    // calculate the velocity needed to reach the player
    float projectileVelocity = distanceToPlayer / (Mathf.Sin(2 * fireAngle * Mathf.Deg2Rad) / gravity);

    // calculate the x and y velocities
    float vx = projectileVelocity * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
    float vy = projectileVelocity * Mathf.Sin(fireAngle * Mathf.Deg2Rad);

    // set the initial velocity of the cannonball
    Rigidbody cannonBallRigidbody = newCannonBall.GetComponent<Rigidbody>();
    cannonBallRigidbody.AddForce(transform.forward * vx + transform.up * vy, ForceMode.VelocityChange);

    // rotate the cannonball towards the player
    newCannonBall.transform.LookAt(playerPosition);
}

}
