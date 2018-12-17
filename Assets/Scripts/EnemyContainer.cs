using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour {
    public int EnemiesLeft;

	// Use this for initialization
	void Start () {
        EnemiesLeft = transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnemyKilled()
    {
        if(EnemiesLeft > 0)
        {
            EnemiesLeft--;
        }
    }
}
