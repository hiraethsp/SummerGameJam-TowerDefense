using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
    Button newgameBtn;
    Button conBtn;
    Button rBtn;
    Button quitBtn;
    private ArchivingAndReading ar;


    void Awake(){
        newgameBtn=transform.GetChild(0).GetComponent<Button>();
        conBtn=transform.GetChild(1).GetComponent<Button>();
        rBtn=transform.GetChild(2).GetComponent<Button>();
        quitBtn=transform.GetChild(3).GetComponent<Button>();

        newgameBtn.onClick.AddListener(NewGame);
        conBtn.onClick.AddListener(conGame);
        rBtn.onClick.AddListener(Rank);
        quitBtn.onClick.AddListener(Quit);
            }

    void NewGame(){
        //Start new game
        Debug.Log("新游戏");
        PanelManager.Instance.openPanel(4);
        PanelManager.Instance.closePanel(1);
        Time.timeScale = 1f;

    }
            void conGame(){
            PanelManager.Instance.openPanel(3);
            PanelManager.Instance.closePanel(1);
            }
            void Rank(){
                //goto rank scene/menu
                // Debug.Log("排行榜");
                PanelManager.Instance.openPanel(2);
                
            }
            void Quit(){
                //Quit game
                // Debug.Log("退出游戏");
                UnityEngine.Application.Quit();
    }
}
