using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
	public AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "Laser")
        {
			AudioSource.PlayClipAtPoint(hitSound, new Vector3(0, 0, -10), 0.75f);
			Destroy(gameObject);
        }
    }
}
