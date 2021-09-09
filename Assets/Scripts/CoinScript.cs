using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) { 
        if (collision.CompareTag("Player")) {
            PlayerMovement p = collision.gameObject.GetComponent<PlayerMovement>();
            p.coinsCollected++;
            Destroy(gameObject);
        }
    }
}
