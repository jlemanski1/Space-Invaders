using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int speed;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImg;

	void Start () {
        speed = 30;
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.up * speed;    // Shoot bullet upwards
	}


    // Handles Collision
    private void OnTriggerEnter2D(Collider2D col) {
        // Bullet hits wall
        if (col.tag == "Wall") {
            Destroy(gameObject);
        }

        // Bullet hits alien
        if (col.tag == "Alien") {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);    // Play sound
            IncreaseTextUIScore();    // Increase score
            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImg;   // Switch to exploded sprite
            Destroy(gameObject);    // Destroy bullet
            DestroyObject(col.gameObject, 0.5f);    // Destroy Alien after 1/2 sec
        }

        // Bullet hits shield
        if (col.tag == "Shield") {
            Destroy(gameObject);              // Destroy bullet
            DestroyObject(col.gameObject);    // Destroy Shield
        }
    }


    // Destroy bullets when no longer visible
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    // Increases Player Score
    private void IncreaseTextUIScore() {
        var textUI = GameObject.Find("Score").GetComponent<Text>();

        int score = int.Parse(textUI.text);

        // Update score & text UI
        score += 10;
        textUI.text = score.ToString();
    }


}
