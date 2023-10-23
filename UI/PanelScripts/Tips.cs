using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tips : MonoBehaviour
{
    public Button backBtn;
 void Awake() {
        backBtn.onClick.AddListener(back);
    }
    void back(){
        PanelManager.Instance.openPanel(5);
        PanelManager.Instance.closePanel(6);
    }
}
