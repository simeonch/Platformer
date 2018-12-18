using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField]
    float walkingSpeed = 1f;
    bool knockbacked = false;
    public int dir;
    bool dead = false;


    public GameObject point;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (dead)
        //{
        //    return;
        //}

        //if (!dead)
        //{ 
        //}

        //velocity checks for better knockback behaviour
        if (rb.velocity.y > 0)
        {
            knockbacked = true;
            return;
        }
        if (rb.velocity.y == 0) //rb.IsTouchingLayers(LayerMask.GetMask("Ground"))
        {
            knockbacked = false;
            if (IsFacingRight())
            {
                rb.velocity = new Vector2(walkingSpeed, 0);
                dir = 1;
            }
            else
            {
                rb.velocity = new Vector2(-walkingSpeed, 0);
                dir = -1;
            }
        }
        Die();
    }

    public void KnockbackMe(Vector2 force)
    {
        rb.AddForce(force);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //compare tag
        //only turn around if enemy isnt knockbacked, trigger exit is not a weapon, player or the player detection radius + polymorph aim (finish)
        if (!knockbacked && collision.tag != "weapon" && collision.tag != "Player" && collision.tag != "Finish" && collision.tag != "enemy")
        {
            ChangeDir();
        }
    }

    public void ChangeDir()
    {
        transform.localScale = new Vector3( -transform.localScale.x ,1f, 1f);
        //transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }

    private void Die()
    {
        if (!dead && rb.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            //Debug.Log(gameObject.name + " died.");
            dead = true;
            transform.position = point.transform.position;
            rb.velocity = Vector2.zero;
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            if (!GetComponent<Enemy2>().PermaPoly)
            {
                transform.parent.GetComponent<EnemyContainer>().EnemyKilled();
            }

            //rb.velocity = Vector2.zero;
            //Animator anim = GetComponent<Animator>();
            //anim.enabled = false;
            //for(int i = 0; i < transform.childCount; i++)
            //{
            //    transform.GetChild(i).gameObject.SetActive(false);
            //}
            ////animator.SetTrigger("Die");
        }
    }

}
