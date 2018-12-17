using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float dir = transform.parent.localScale.x;
            if((collision.transform.position.x < transform.position.x && dir > 0) || (collision.transform.position.x > transform.position.x && dir < 0))
            {
                transform.parent.GetComponent<EnemyController>().ChangeDir();
            }
            //transform.parent.GetComponent<EnemyController>().ChangeDir();
        }
    }
}
