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

    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D rb;
    Animator animator;
    Collider2D collider;

    //Message then methods
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Run();
        FlipSprite();
        Jump(); 

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
        //if ()
        //{
        //    return;
        //}

        if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
            CrossPlatformInputManager.GetButtonDown("Jump") )
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
            rb.velocity += jumpVelocityToAdd;
        }

        if (rb.velocity.y != 0)
        {
            print(rb.velocity.y);
        }

        //animate jumping
        bool isJumping = Mathf.Abs(rb.velocity.y) > 1;
        animator.SetBool("isJumping", isJumping);
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
