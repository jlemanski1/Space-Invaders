using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int speed;

    private Rigidbody2D rigidBody;


	void Start () {
        speed = 30;
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.up * speed;    // Shoot bullet upwards
	}


    // Destroy bullets after hitting wall
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Wall") {
            Destroy(gameObject);
        }
    }


    // Destroy bullets when no longer visible
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
