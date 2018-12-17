using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollision : MonoBehaviour {

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //enemies, being dynamic now push the player and onlayertouch doesnt work so else
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //use compare tag, more effective
        //turn around if you collide with terrain, else kill player
        if (collision.collider.tag != "Player")
        {
            //Debug.Log(collision.collider.name);
            //transform.parent.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
            transform.parent.GetComponent<EnemyController>().ChangeDir();
        }
        else
        {
            collision.collider.gameObject.GetComponent<Player2>().Death();
        }
    }
}
