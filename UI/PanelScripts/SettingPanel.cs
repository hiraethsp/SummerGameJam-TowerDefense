using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : PanelManager2
{
    private PanelManager2 panelManager;
    public AudioSource musicSource;

    Button backBtn;
    Button musicBtn;
    Button BackMain;
    bool musicswtich=false;
    void Awake(){
        backBtn=transform.GetChild(0).GetComponent<Button>();
        musicBtn=transform.GetChild(1).GetComponent<Button>();
        BackMain=transform.GetChild(2).GetComponent<Button>();


        backBtn.onClick.AddListener(back);
        musicBtn.onClick.AddListener(music);
        BackMain.onClick.AddListener(BM);

        panelManager = GetComponent<PanelManager2>();

        GameObject gameManager = GameObject.FindWithTag("GameController");
        if (gameManager != null)
        {
            musicSource = gameManager.GetComponent<AudioSource>();
        }
    }

    void back(){
        panelManager.closePanel(2);
    }

    void music()
    {
        // ÇÐ»»ÒôÀÖµÄ¿ª¹Ø×´Ì¬
        musicSource.mute = !musicSource.mute;

    }

    void BM(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
