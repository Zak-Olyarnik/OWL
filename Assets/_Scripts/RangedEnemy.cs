using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController {

    public float range;
    public float fireRate;
    public Rigidbody2D coffeePrefab;
    //[SerializeField] protected AnimController anim;
    private bool isFiring;
	//private GameController gc;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
		gc = GameController.GetInstance();
        isFiring = false;
    }

    public override void MoveEnemy() {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 movement = (target.position - transform.position).normalized;
        rotAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotAngle, Vector3.forward);
        anim.setSpeed(movement.magnitude * speed);

        if (distance < range && !isFiring)
        {
            speed = 0.0f;
            isFiring = true;
            InvokeRepeating("attack", 0.0f, fireRate);
            anim.attackStart();
        }
    }

    protected override void attack() {
        Instantiate(coffeePrefab, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint(attackSound, new Vector3(0, 0, -10), 0.75f);
    }
}
