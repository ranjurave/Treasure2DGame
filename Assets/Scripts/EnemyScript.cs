using UnityEngine;

public class EnemyScript : MonoBehaviour {
    Rigidbody2D enemyRigidBody;
    public float moveSpeed = 2;
    int direction = 1;

    void Start() {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        enemyRigidBody.velocity = new Vector2(moveSpeed * direction, 0);
    }

    // Flipping the character when it hits a wall
    private void OnTriggerEnter2D(Collider2D collision) {
        direction *= -1;
        transform.localScale = new Vector2(Mathf.Sign(direction) * 4, 4);
    }
}
