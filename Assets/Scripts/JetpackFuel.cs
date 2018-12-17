using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackFuel : MonoBehaviour {
    public int Fuel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player2>().PowerJetpack(Fuel);
            this.gameObject.SetActive(false);
        }
    }
}
