using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingWeapon : MonoBehaviour {

    //private Rigidbody2D rb;
    private float timeActive = 0;
    //private int weaponDir;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody2D>();
        //weaponDir = transform.parent.parent.GetComponent<EnemyController>().dir;
        //rb.velocity = new Vector2(3.0f * weaponDir, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        timeActive += Time.deltaTime;

        transform.Rotate(new Vector3(0f, 0f, 1f), 5.0f);
        if(timeActive >= 1.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player2>().StunPlayer();
            Destroy(gameObject);
        }
        if(collision.tag == "enemy")
        {
            //Debug.Log("hit enemy");
            collision.GetComponent<Enemy2>().PolymorphStun(0.5f);
            Destroy(gameObject);
        }
    }
}
