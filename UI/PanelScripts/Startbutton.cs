using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Startbutton : PanelManager
{
    Button startBtn;
    private PanelManager panelManager;
    void Awake()
    {
        startBtn = transform.GetChild(0).GetComponent<Button>();
        startBtn.onClick.AddListener(startgame);
        panelManager = transform.parent.GetComponent<PanelManager>();
    }
    void startgame()
    {
        panelManager.openPanel(1);
        panelManager.closePanel(0);
    }
}