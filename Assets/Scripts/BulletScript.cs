using UnityEngine;

public class BulletScript : MonoBehaviour {
    float speed = 20f;
    Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground")) {
            Destroy(gameObject);
        }
    }
}
