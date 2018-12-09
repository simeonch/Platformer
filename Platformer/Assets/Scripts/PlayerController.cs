using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    //Config
    [SerializeField]
    float speed =  5f;

    [SerializeField]
    float jumpForce = 5f;

    [SerializeField]
    Vector2 deathKick = new Vector2(2, 2);

    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feet;


    //Message then methods
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        Jump();
        Interact();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1/1
        Vector2 velocity = new Vector2(controlThrow * speed, rb.velocity.y);
        rb.velocity = velocity;

        //animate running
        bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", isRunning);
    }

    private void Jump()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")) 
            && CrossPlatformInputManager.GetButtonDown("Jump") )
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
            rb.velocity += jumpVelocityToAdd;
        }

        //animate jumping
        int verticalVelocity = (int)rb.velocity.y;
        animator.SetInteger("VerticalVelocity", verticalVelocity);
    }

    private void Interact()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Interactables")) 
            && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            Debug.Log("such interaction, much wow");
        }
    }

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Die");
            rb.velocity = deathKick;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        bool isMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
        }

    }
}
