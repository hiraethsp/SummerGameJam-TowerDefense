using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Writestory : MonoBehaviour
{
    [SerializeField]public Button readarchiveBtn1;
    [SerializeField]public Button readarchiveBtn2;
    [SerializeField]public Button readarchiveBtn3;
    [SerializeField]public Button backBtn;
    [SerializeField]public GameObject nameinput;
    void Start(){
        backBtn.onClick.AddListener(back);
        readarchiveBtn1.onClick.AddListener(namein_1);
        readarchiveBtn2.onClick.AddListener(namein_2);
        readarchiveBtn3.onClick.AddListener(namein_3);
    }
    void back(){
        PanelManager.Instance.closePanel(4);
        PanelManager.Instance.openPanel(1);
    }
    void namein_1(){
        nameinput.SetActive(true);
        Temp.storyNum = 1;
    }

    void namein_2(){
        nameinput.SetActive(true);
        Temp.storyNum = 2;
    }

    void namein_3(){
        nameinput.SetActive(true);
        Temp.storyNum = 3;
    }
    

    
}
