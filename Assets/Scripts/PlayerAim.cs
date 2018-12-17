using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

    public float AimSpeed;
    private float dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dir = transform.parent.localScale.x;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(new Vector3(0, 0, 1 * dir), AimSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(new Vector3(0, 0, -1 * dir), AimSpeed * Time.deltaTime);
        }

        //if (transform.eulerAngles.z > -50f && transform.eulerAngles.z < 80f)
        //{
        //}
    }

}
