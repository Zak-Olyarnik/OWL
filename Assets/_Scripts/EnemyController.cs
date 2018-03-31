using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public AudioClip attackSound;
	public AudioClip deathSound;
    public float speed;
    public float swingRate;
    public int damage;
    protected Transform target;
    protected float rotAngle;
    [SerializeField] protected AnimController anim;
    private bool dead;
    protected GameController gc;
    protected Collider2D col;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        gc = GameController.GetInstance();
    }
	
	// Update is called once per frame
    void Update () {
        MoveEnemy();

        if (anim.getAttacking())
        {
           // Debug.Log("Currently punching.");
        }

        if (anim.actuallyDie())
        {
			AudioSource.PlayClipAtPoint(deathSound, new Vector3(0, 0, -10), 0.5f);
            gc.currentEnemies--;
            DestroyObject(this.gameObject);
        }

    
    }

    virtual public void MoveEnemy() {
        Vector3 movement = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        rotAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotAngle, Vector3.forward);
        anim.setSpeed(movement.magnitude * speed);
    }

	virtual public void Die()
	{
		anim.die();
	}

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Player")
		{
            InvokeRepeating("attack", 0.0f, swingRate);
        }
    }

    virtual protected void attack() {
        speed = 0.0f;
        PlayerController.GetInstance().Damage(damage);
        anim.attackStart();
		AudioSource.PlayClipAtPoint(attackSound, new Vector3(0, 0, -10), 0.75f);
    }
}
