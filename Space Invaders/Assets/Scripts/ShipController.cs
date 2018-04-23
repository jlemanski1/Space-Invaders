using UnityEngine;

public class ShipController : MonoBehaviour {

    [SerializeField]
    private int speed;

    public GameObject bullet;


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
            Instantiate(bullet, transform.position, Quaternion.identity);

            // Bullet Sound effect
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.bulletFire);
        }
    }

}
