using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    int curlife;
    float curtimer;
    int maxlife;
    [SerializeField]public TMP_Text lifeHUD;
    [SerializeField]public TMP_Text timeHUD;
    void Start()
    {
        maxlife = CS_GameManager.Instance.myMaxLife;
    }
    void Update()
    {
        curlife = CS_GameManager.Instance.getMyHealth();
        curtimer = 180 - (int)timeManager.Instance.getTime();
        if(curtimer>=0){
        timeHUD.text = "Time:" + curtimer + "s";}
        lifeHUD.text = "Life:" + curlife + "/" + maxlife;
    }
}
