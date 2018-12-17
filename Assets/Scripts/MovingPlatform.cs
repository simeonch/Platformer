using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    //public Transform pt1;
    //public Transform pt2;
    //public Vector3 point1;
    //public Vector3 point2;

    public Vector3[] points;
    private Vector3 nextPoint;

    //private bool changeDir = false;
    //private Rigidbody2D rb;
    public int k = 0;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody2D>();
        nextPoint = points[k];
	}
	
	// Update is called once per frame
	void Update () {
        //if(!changeDir)
        //      {
        //          transform.localPosition = Vector2.MoveTowards(transform.localPosition, point1, 2.0f * Time.deltaTime);
        //          if(transform.position == point1)
        //          {
        //              changeDir = true;
        //          }
        //      }
        //      else
        //      {
        //          transform.localPosition = Vector2.MoveTowards(transform.localPosition, point2, 2.0f * Time.deltaTime);
        //          if(transform.position == point2)
        //          {
        //              changeDir = false;
        //          }
        //      }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, nextPoint, 2.0f * Time.deltaTime);
        if (transform.position == nextPoint) // && k <= points.Length - 1
        {
            //Debug.Log("Point " + k + " = " + nextPoint + " reached");
            CyclePoints();
        }
    }

    //loop
    private void CyclePoints()
    {
        k++;
        if(k == points.Length)
        {
            k = 0;
        }
        nextPoint = points[k];
        //Debug.Log(nextPoint); 
    }


    //collide with other objects, make player stay
    //better to not have collisions or make better routes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
        {
            CyclePoints();
        }
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    //get rid of player
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.transform.parent = transform.parent;
        }
    }

}
