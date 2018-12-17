using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

    public EnemyContainer EC;
    private Text text;
    private int totalEnemies;

	// Use this for initialization
	void Start () {
        text = transform.GetChild(0).GetComponent<Text>();
        totalEnemies = EC.EnemiesLeft;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = EC.EnemiesDown.ToString() + " / " + totalEnemies.ToString();
	}
}
