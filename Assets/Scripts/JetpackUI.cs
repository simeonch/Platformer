using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackUI : MonoBehaviour
{
    public Player2 Player;

    private Image thisIMG;
    public float filler;


    public float MapNumber(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    // Use this for initialization
    void Start()
    {
        thisIMG = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        filler = MapNumber(Player.Jetpack, 0.0f, 10.0f, 0.0f, 1.0f);
        thisIMG.fillAmount = filler;
    }
}
