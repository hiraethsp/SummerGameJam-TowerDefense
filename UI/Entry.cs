using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entry : MonoBehaviour
{
    
    public Button entryBtn;
    void Awake(){
    entryBtn.onClick.AddListener(entry);
    }
    void entry(){
        PanelManager.Instance.openPanel(5);
        PanelManager.Instance.closePanel(1);
    }
}
