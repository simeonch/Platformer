using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour {
    public Player2 Player;
    public EnemyContainer Enemies;
    public float levelTime;

    public float bronzeTimeLeft;
    public float silverTimeLeft;
    public float goldTimeLeft;

    private Text BlastText;
    private Image JetPackImage;
    private float JetPackFill;
    private Text EnemyText;
    private float totalEnemies;
    private Text LevelTimer;

    public float MapNumber(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    // Use this for initialization
    void Start () {
        BlastText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        JetPackImage = transform.GetChild(1).GetComponent<Image>();
        EnemyText = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        totalEnemies = Enemies.EnemiesLeft;

        LevelTimer = transform.GetChild(3).GetComponent<Text>();

        //levelTime = 62.0f;
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
            BlastText.text = Player.Ammo.ToString();
            JetPackFill = MapNumber(Player.Jetpack, 0.0f, 10.0f, 0.0f, 1.0f);
            JetPackImage.fillAmount = JetPackFill;

            //EnemyText.text = Enemies.EnemiesDown.ToString() + " / " + totalEnemies.ToString();
            EnemyText.text = Enemies.EnemiesLeft.ToString();

            if (levelTime > 0)
            {
                levelTime -= Time.deltaTime;
                LevelTimer.text = levelTime.ToString("0"); //0.## but it's annoying
            }
            else
            {
                levelTime = 0;
                LevelTimer.text = "time over";
            }
        }
        else if (Player.isAlive)
        {
            string res;
            if (levelTime >= goldTimeLeft)
            {
                res = "Gold";
            }
            else if (levelTime >= silverTimeLeft)
            {
                res = "Silver";
            }
            else
            {
                res = "Bronze";
            }
            Debug.Log("Game won with " + res + ", " + levelTime);

            SceneManager.LoadScene("Success Menu", LoadSceneMode.Single);
        }
	}

}
