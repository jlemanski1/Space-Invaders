using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour {

    private Rigidbody2D rigidBody;

    [SerializeField]
    private float speed;

    public Sprite explodedShipImg;

	void Start () {
        speed = 30;
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.down * speed;    // Shoot bullet down
    }

    // Handles Alien bullet collisions
    private void OnTriggerEnter2D(Collider2D col) {
        // Hits Walls
        if (col.tag == "Wall") {
            Destroy(gameObject);    // Destroy bullet
        }

        // Alien hits player
        if (col.gameObject.tag == "Player") {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion); //Play sound
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImg;    // Change to death sprite
            Destroy(gameObject);    // Destroy bullet
            DestroyObject(col.gameObject, 0.5f);    // Destroy player ship after .5 secs
        }

        // Alien hits shield
        if (col.gameObject.tag == "Shield") {
            Destroy(gameObject);
            DestroyObject(col.gameObject);
        }
    }

    // Destroy bullets when no longer visible
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
