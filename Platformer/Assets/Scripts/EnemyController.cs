using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField]
    float walkingSpeed = 1f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(walkingSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-walkingSpeed, 0);
        }
	}

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }
}
