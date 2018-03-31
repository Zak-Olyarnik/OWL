using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour {

    public float delayTime;
    private float speed;

	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            speed = this.transform.parent.gameObject.GetComponent<EnemyController>().speed;
            this.transform.parent.gameObject.GetComponent<EnemyController>().speed = 0.0f;
            Invoke("delay", delayTime);
        }
    }

    private void delay() {
        this.transform.parent.gameObject.GetComponent<EnemyController>().speed = speed;
    }
}
