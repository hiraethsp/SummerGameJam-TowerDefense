using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Lobby : PanelManager
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
        ar = GetComponent<ArchivingAndReading>();
            }

    void NewGame(){
        //Start new game
        Debug.Log("新游戏");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Final");
        Time.timeScale = 1f;
        CloseSetting.Instance.closeSetting();
    }
            void conGame(){
                //Start game
                Debug.Log("继续游戏");
                ar.LoadByJSON();
            }
            void Rank(){
                //goto rank scene/menu
                Debug.Log("排行榜");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Rank");
            }
            void Quit(){
                //Quit game
                Debug.Log("退出游戏");
                UnityEngine.Application.Quit();
    }
}
