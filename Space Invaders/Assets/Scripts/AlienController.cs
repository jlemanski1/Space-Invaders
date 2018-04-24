using System.Collections;
using UnityEngine;

public class AlienController : MonoBehaviour {

    [SerializeField]
    private int speed;

    private Rigidbody2D rigidBody;

    // Alien's Sprites
    public Sprite baseImg;
    public Sprite altImg;

    public Sprite explodedShipImg;      // Dead player sprite

    private SpriteRenderer spriteRenderer;

    public float spriteSwapDelay = 0.5f;    // Seconds betwen switching sprites
    public float minFireRate = 1.0f;        // Minimum secs between firing
    public float maxFireRate = 3.0f;        // Maximum secs between firing
    public float baseFireRate = 3.0f;       // Base Rate

    public GameObject alienBullet;

	void Start () {
        speed = 10;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(1, 0) * speed;     // Start Aliens off moving to the right

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(SwapAlienSprite());      // Cycles between alien sprites w/ sound effects

        baseFireRate = baseFireRate + Random.Range(minFireRate, maxFireRate);       // Set base fire rate
    }
    

    // Collision Handling
    private void OnCollisionEnter2D(Collision2D col) {
        // Hit left wall, turn right
        if (col.gameObject.name == "Left_Wall") {
            Turn(1);    // Turn to the right
            MoveLower();    // Lower alien
        }

        // Hit right wall, turn left
        if (col.gameObject.name == "Right_Wall") {
            Turn(-1);    // Turn to the left
            MoveLower();    // Lower alien
        }

    }


    // Change Alien's direction
    private void Turn(int direction) {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;      // Set X direction
        rigidBody.velocity = newVelocity;       // Set new velocity
    }


    // Lower alien closer to player ship
    private void MoveLower() {
        Vector2 position = transform.position;
        position.y -= 1;    // Lower by 1 unit
        transform.position = position;      // Set new position
    }


    // Cycles between alien sprites with sound effects
	public IEnumerator SwapAlienSprite() {
        while (true) {
            if (spriteRenderer.sprite == baseImg) {
                spriteRenderer.sprite = altImg;     // Swap sprite to alt

                if (SoundManager.Instance)
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);    // Play sound effect
            }
            else {
                spriteRenderer.sprite = baseImg;     // Swap sprite back to base
                if (SoundManager.Instance)
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz2);    // Play sound effect
            }

            yield return new WaitForSeconds(spriteSwapDelay);   // Wait before swapping sprites
        }
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);     // Play sound effect
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImg;    // Change to exploded ship image
            Destroy(gameObject);
            DestroyObject(col.gameObject, 0.5f);      // Destroy the ship after .5 secs
        }
    }


    private void FixedUpdate() {
        // Randomly fire bullet at player
        if (Time.time > baseFireRate) {
            baseFireRate = baseFireRate + Random.Range(minFireRate, maxFireRate);
            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }
}
