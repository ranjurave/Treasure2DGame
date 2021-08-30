using UnityEngine;
public class PlayerMovement : MonoBehaviour {
    Rigidbody2D playerRB;
    public float moveSpeed;
    Animator playerAnimator;
    BoxCollider2D jumpCollider;

    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        jumpCollider = GetComponent<BoxCollider2D>();
    }
    void Update() {
        Move();
        Jump();
    }

    private void Move() {
        //Player movements
        float movement = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(movement * moveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;

        // To flip the character when moving left
        bool playerHasHoizontalMovment = Mathf.Abs(playerRB.velocity.x) > 0;
        if (playerHasHoizontalMovment) {
            transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), 1);
        }

        //Change Boolean parameter in Animator Controller to play walk animation
        playerAnimator.SetBool("CanWalk", playerHasHoizontalMovment);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (jumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
                playerAnimator.SetBool("CanJump", true);
                playerRB.velocity += new Vector2(0, 5);
            }
        }
        else {
            playerAnimator.SetBool("CanJump", false);
        }
    }
}
