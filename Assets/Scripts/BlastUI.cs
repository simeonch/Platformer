using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlastUI : MonoBehaviour {

    public Player2 Player;
    private Text text;

	// Use this for initialization
	void Start () {
        text = transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Player.Ammo.ToString();
    }
}
