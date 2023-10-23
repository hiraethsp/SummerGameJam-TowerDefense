using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingame : MonoBehaviour
{
    private const bool V = true;
    [SerializeField] Button settingBtn;
    [SerializeField] Button pauseBtn;
    [SerializeField] Button bagBtn;
    [SerializeField] Sprite pausebg;
    [SerializeField] Sprite playbg;
    [SerializeField] Image pauseb;
    bool bagstate=false;
    bool ifPause = false;


    void Start(){
        settingBtn.onClick.AddListener(setting);
        pauseBtn.onClick.AddListener(pause);
        bagBtn.onClick.AddListener(bag);
    }
    void setting(){
        PanelManager2.Instance.openPanel(2);
    }
    void bag(){
        if(bagstate==false){
            bagstate= V;
            PanelManager2.Instance.openPanel(0);
    }
    else{
        bagstate=false;
        PanelManager2.Instance.closePanel(0);
    }
    }

    void pause(){
       
        if (!ifPause)
        {
            CS_GameManager.Instance.onPause=true;
            pauseb.sprite=pausebg;
        }
        else
        {
            CS_GameManager.Instance.onPause=false;
            pauseb.sprite=playbg;
        }
        ifPause = !ifPause;
    }
}
