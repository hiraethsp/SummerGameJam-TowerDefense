using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLine : MonoBehaviour
{
    public Button readarchiveBtn1;
    public Button readarchiveBtn2;
    public Button readarchiveBtn3;
    public Button backBtn;
    void Awake(){
        backBtn.onClick.AddListener(back);
    }
    void back(){
        PanelManager.Instance.closePanel(3);
        PanelManager.Instance.openPanel(1);
    }
}
