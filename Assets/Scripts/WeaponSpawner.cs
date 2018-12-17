using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject spawnable;
    private float timer = 0.0f;
    public float SpawnTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < SpawnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            GameObject ga = Instantiate(spawnable, transform.position, Quaternion.identity, transform.parent.parent);
            ga.SetActive(true);
            Rigidbody2D rb = ga.GetComponent<Rigidbody2D>();

            //if it's a spawn point from an enemy or a platform
            if (transform.parent.GetComponent<EnemyController>() != null)
            {
                int weaponDir = transform.parent.GetComponent<EnemyController>().dir;
                rb.velocity = new Vector2(3.0f * weaponDir, 0.0f);
            }
            else
            {
                rb.velocity = new Vector2(-3.0f, 0.0f);
            }
        }
    }
}
