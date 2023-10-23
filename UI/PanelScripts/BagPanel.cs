using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : PanelManager
{
    private PanelManager panelManager;

    void Awake(){
        //closeBtn=transform.GetChild(4).GetComponent<Button>();
        //closeBtn.onClick.AddListener(close);
        panelManager = GetComponent<PanelManager>();
    }
}
