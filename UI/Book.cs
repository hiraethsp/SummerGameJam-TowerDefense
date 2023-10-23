using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public Button itemBtn;
    public Button towerBtn;
    public Button enemyBtn;
    public Button backBtn;
    void Awake(){
        itemBtn.onClick.AddListener(item);
        towerBtn.onClick.AddListener(tower);
        enemyBtn.onClick.AddListener(enemy);
        backBtn.onClick.AddListener(back);
    }
    void item(){}
    void tower(){}
    void enemy(){}
    void back(){
        PanelManager.Instance.openPanel(5);
        PanelManager.Instance.closePanel(7);
    }

}
