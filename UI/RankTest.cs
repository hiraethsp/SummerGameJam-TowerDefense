 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;

 public class RankTest : MonoBehaviour
 {
      RankManager rankManager;


     public Button backkBtn;

   void Awake(){
        backkBtn.onClick.AddListener(Back);
                    } 
    void Start()
    {
        rank();
    }

    void Back(){
        PanelManager.Instance.closePanel(2);
    }

     
     void rank()
     {
         rankManager = RankManager.instance;
         rankManager.Start();
         Debug.Log(rankManager);
         rankManager.Initiate();
        //  Debug.Log("生成排行榜");
     }

 }
