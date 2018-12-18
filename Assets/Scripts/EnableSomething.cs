using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSomething : MonoBehaviour {

    public GameObject toEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        toEnable.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
