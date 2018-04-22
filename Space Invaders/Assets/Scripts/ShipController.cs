using UnityEngine;

public class ShipController : MonoBehaviour {

    [SerializeField]
    private int speed;

    //Public GameObject bullet;

    

    private void Start () {
        speed = 30;
	}

    // Spaceship movement
    private void FixedUpdate() {
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove, 0) * speed;
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            // Fire bullet
            // Bullet Sound effect
        }
    }

}
