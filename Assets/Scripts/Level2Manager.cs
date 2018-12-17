using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour {
    public Player2 Player;
    public EnemyContainer Enemies;
    public float levelTime;

    private bool GameWon = false;

    public float bronzeTimeLeft;
    public float silverTimeLeft;
    public float goldTimeLeft;

	// Use this for initialization
	void Start () {
        levelTime = 14.0f;
	}
	
    //rework

	// Update is called once per frame
	void Update () {
        if(!Player.isAlive)
        {
            Debug.Log("Lost");
            return;
        }
        if (Enemies.EnemiesLeft > 0)
        {
            levelTime -= Time.deltaTime;
        }
        else if (Player.isAlive)
        {
            Debug.Log("Game won");
            SceneManager.LoadScene("Success Menu", LoadSceneMode.Single);
            //float margin;
            //if(levelTime <= silverTimeLeft)
            //{
            //    margin = levelTime - silverTimeLeft;
            //    if(levelTime < bronzeTimeLeft)
            //    {
            //        Debug.Log("bronze");
            //    }

            //    //closer to:
            //    float marginup = levelTime - bronzeTimeLeft;
            //    if (margin < marginup)
            //    {
            //        Debug.Log("silver");
            //    }
            //    else
            //    {
            //        Debug.Log("bronze");
            //    }

            //}
            //else
            //{
            //    margin = levelTime - silverTimeLeft;

            //    if(levelTime >= goldTimeLeft)
            //    {
            //        Debug.Log("gold");
            //    }

            //    //closer to:
            //    float marginup = levelTime - goldTimeLeft;
            //    if(margin < marginup)
            //    {
            //        Debug.Log("gold");
            //    }
            //    else
            //    {
            //        Debug.Log("silver");
            //    }

            //}
        }
	}

}
