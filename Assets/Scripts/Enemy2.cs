using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

    public float PolymorphDuration;
    public float compareTime;
    private bool isPolymorphed = false;
    public bool PermaPoly = false;

    public Sprite defaultSprite;
    public Sprite polySprite;

    public void PolymorphMe()
    {
        compareTime = 0;
        isPolymorphed = true;
        compareTime += PolymorphDuration;
        Poly();
    }

    public void PolymorphStun(float duration)
    {
        compareTime = 0;
        isPolymorphed = true;
        compareTime += duration;
        Poly();
    }

    //turn enemy into perma terrain (ish)
    public void PolymorphPerma()
    {
        if (!PermaPoly)
        {
            PermaPoly = true;
            gameObject.transform.parent.GetComponent<EnemyContainer>().EnemyKilled();

            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<EnemyController>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = polySprite;
            gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            gameObject.tag = "polymorphed";
            transform.GetChild(3).gameObject.tag = "polymorphed";
            transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }

    //turn enemy back to normal
    private void UnPolymorph()
    {
        compareTime = 0;
        isPolymorphed = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<EnemyController>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(1).GetComponent<WeaponSpawner>().enabled = true;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);

        gameObject.tag = "enemy";
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        transform.GetChild(3).gameObject.SetActive(false);
    }

    //may be moved to PolymorphMe()
    private void Poly()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<EnemyController>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = polySprite;
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).GetComponent<WeaponSpawner>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        gameObject.tag = "polymorphed";
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        transform.GetChild(3).gameObject.SetActive(true);

        //maybe make this by default in the prefab:
        transform.GetChild(3).gameObject.tag = "polymorphed";
        transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer("Ground");
    }

    // Use this for initialization
    void Start()
    {
        defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPolymorphed && !PermaPoly)
        {
            //polymorph timer
            if (compareTime > 0)
            {
                compareTime -= Time.deltaTime;
            }
            else
            {
                UnPolymorph();
            }
        }
    }

}
