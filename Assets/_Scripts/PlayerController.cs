using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Singleton
	public static PlayerController instance;
	public static PlayerController GetInstance() { return instance; }
	void Awake() { instance = this; }

    //public GameObject root;
	public AudioClip laserSound;
	public AudioClip flySound;
    private Vector2 movement;
    private Vector3 target;
    private float rotAngle;
    private Quaternion rotation;
    private int timeToFire = 0;
    private bool moving = false;
    private Vector3 moveTarget;
    private int health;
    private int playerPosition;

    private Vector3[] trees = new[] {
       new Vector3(0,0,0),
       new Vector3(-7.0f, 3.5f,0),
       new Vector3(7.0f, 3.5f,0),
       new Vector3(7.0f, -3.5f,0),
       new Vector3(-7.0f, -3.5f,0)
    };

    [SerializeField] private int speed; 
    [SerializeField] private int laserSpeed;
    [SerializeField] private int fireDelay;
    [SerializeField] private int healthMax;

    [SerializeField]private GameObject laser;

	// Use this for initialization
	void Start () {
        playerPosition = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameController.isPaused)
		{
			// get cursor position on screen
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = 0f;

			// rotate sprite to cursor
			movement = new Vector2(0f, 0f);
			movement = (target - transform.position).normalized;
			rotAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(rotAngle, Vector3.forward);

			// shoot laser
			if (Input.GetButton("Fire1"))
			{
				checkLaserFire();
			}

			// move position to next tree
			if (Input.GetButton("Fire2") && !moving)
			{
				moving = true;
				playerPosition += 1;
				if (playerPosition == 5)
					playerPosition = 0;
				moveTarget = trees[playerPosition];
			}

			if (moving)
			{
				PlayerMove();
			}
		}
        
	}


    /* //////////////////////////// */
    /* User written functions below */
    /* //////////////////////////// */

    public int GetHealth()
    {
        return health;
    }

    public int GetHealthMax()
    {
        return healthMax;
    }

    void checkLaserFire() {
        if (timeToFire <= 0) {
            rotation = Quaternion.AngleAxis(rotAngle, Vector3.forward);
            GameObject shot = Instantiate(laser, new Vector3(transform.position.x, transform.position.y), rotation) as GameObject;
            shot.GetComponent<Rigidbody2D>().velocity = (Vector2)(rotation * (Vector2.right * (Time.deltaTime * laserSpeed)));
			AudioSource.PlayClipAtPoint(laserSound, new Vector3(0, 0, -10), 0.1f);
            timeToFire = fireDelay;
        }

        timeToFire--;
    }

    void PlayerMove() {
		AudioSource source = GetComponent<AudioSource>();
		if (!source.isPlaying)
		{ source.Play(); }
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * speed);
		if (Vector3.Distance(transform.position, moveTarget) < .1f)	// stop when very close to target
		{
            movement = new Vector2(0f, 0f);
            moving = false;
			source.Stop();
        }

    }

	//public void ApplyPowerUp()
	//{

	//}

	//public void ApplyUpgrade(string type) {
	//    switch(type.ToLower()) {
	//        case "health":
	//            IncreaseHealth();
	//            break;
	//        case "playerspeed":
	//            IncreaseSpeed();
	//            break;
	//        case "firerate":
	//            IncreaseFireRate();
	//            break;
	//        case "rootpenalty":
	//            //root.SetActive(true);
	//            //Debug.Log("<---To be continued---| RootRadius ");
	//            break;
	//        default:
	//            Debug.Log("We shouldn't be seeing this.");
	//            break;
	//    }
	//}

	//public void IncreaseSpeed()
	//{
	//    speed = (speed * 3) / 2;
	//}

	//public void IncreaseFireRate()
	//{
	//    fireDelay = (fireDelay * 2) / 3;
	//}

	//public void IncreaseHealth()
	//{
	//    healthMax = (healthMax * 5)/4;
	//}

    public void HealthInit() {
        health = healthMax;
    }

    public void Damage(int damage)
    {
        health-=damage;
    }
}