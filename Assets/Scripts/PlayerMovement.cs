using System.Collections;
using UnityEngine;
public class PlayerMovement : MonoBehaviour {
    Rigidbody2D playerRB;
    public float moveSpeed;
    Animator playerAnimator;
    BoxCollider2D jumpCollider;
    CapsuleCollider2D playerCollider;
    public float climbSpeed = 2.0f;
    public int coinsCollected;
    public bool isAlive;
    public LineRenderer shootingLine; // Display line when raycasting to shoot
    public Transform bulletSpawnPoint; // Set an offset position to start the bullet or raycast
    public GameObject bullet;

    void Start() {
        isAlive = true;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        jumpCollider = GetComponent<BoxCollider2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
    
    void Update() {
        if (isAlive) {
            Move();
            Jump();
            Climb();
            Die();
            //Fire();
            StartCoroutine(FireRaycast());
        }
    }

    private void Move() {
        //Player movements
        float movement = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(movement * moveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;

        if (Input.GetAxis("Horizontal") < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetAxis("Horizontal") > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        bool playerHasHoizontalMovment = Mathf.Abs(playerRB.velocity.x) > 0;

        //// To flip the character when moving left
        //if (playerHasHoizontalMovment) {
        //    transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), 1);
        //}

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
            playerAnimator.SetBool("CanJump", false); //Change Boolean parameter in Animator Controller to play jump animation
        }
    }

    private void Climb() {
        // if player is colliding with ladder layer
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {  
            if (Input.GetKey(KeyCode.UpArrow)) {
                Vector2 climbVelocity = new Vector2(playerRB.velocity.x, climbSpeed);
                playerRB.velocity = climbVelocity;
                playerAnimator.SetBool("CanClimb", true);
            }
            else {
                playerRB.gravityScale = 0; // setting gravity to 0 so that the player will not drift down
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0); // stopping the upward push
            }
        }
        else {
            playerRB.gravityScale = 1; // restoring gravity
            playerAnimator.SetBool("CanClimb", false);
        }
    }

    void Die() {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            isAlive = false;
            playerAnimator.SetTrigger("Dead");//Change Trigger parameter in Animator Controller to play dead animation
            playerRB.velocity = new Vector2(10, 10); // Toss the character up in air
            playerCollider.enabled = false; // Disable all inputs
        }
    }

    // Fire with fireball sprite with Rigidbody
    void Fire() { 
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    // Fire with raycast
    IEnumerator FireRaycast() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hitInfo = Physics2D.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.right);
            if (hitInfo) {
                shootingLine.SetPosition(0, bulletSpawnPoint.position);
                shootingLine.SetPosition(1, hitInfo.point);
                EnemyScript enemy = hitInfo.transform.GetComponent<EnemyScript>();
                if (enemy != null) {
                    Destroy(enemy.gameObject);
                }
            }
            else {
                shootingLine.SetPosition(0, bulletSpawnPoint.position);
                shootingLine.SetPosition(1, bulletSpawnPoint.position + bulletSpawnPoint.right * 100);
            }
            shootingLine.enabled = true;
            yield return new WaitForSeconds(0.05f);
            shootingLine.enabled = false; 
        }
    }
}