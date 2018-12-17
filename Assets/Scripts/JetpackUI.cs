using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackUI : MonoBehaviour {

    public float asd;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(GetComponent<SpriteRenderer>().size.x, asd);
    }
}
