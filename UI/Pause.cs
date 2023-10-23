using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Pause : MonoBehaviour
{
    private static Pause instance = null;
    public static Pause Instance { get { return instance; } }
    Button setBtn;
    Button homeBtn;
    Button pauseBtn;
    Camera mainCamera;
    Camera settingCamera;
    Canvas UICanvas;


    //bool visible=false;
    bool ifPause = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        pauseBtn = transform.GetChild(0).GetComponent<Button>();
        homeBtn = transform.GetChild(1).GetComponent<Button>();
        setBtn = transform.GetChild(2).GetComponent<Button>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        settingCamera = GameObject.Find("Setting Camera").GetComponent<Camera>();
        UICanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        //GameObject.Find("rank").GetComponent<Button>();

        //reBtn.gameObject.SetActive(false);
        //homeBtn.gameObject.SetActive(false);
        //setBtn.gameObject.SetActive(false);

        setBtn.onClick.AddListener(setting);
        homeBtn.onClick.AddListener(toHome);
        pauseBtn.onClick.AddListener(pauseGame);
    }

    void pauseGame()
    {
        //Show other buttons
        Debug.Log("暂停");
        //if (!visible)
        //{
         //   reBtn.gameObject.SetActive(true);
        //    homeBtn.gameObject.SetActive(true);
         //   setBtn.gameObject.SetActive(true);
      //  }
       // else
       // {
            //reBtn.gameObject.SetActive(false);
            //homeBtn.gameObject.SetActive(false);
           // setBtn.gameObject.SetActive(false);
       // }
      //  visible = !visible;
        if (!ifPause)
        {
            Debug.Log("暂停游戏");
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("继续游戏");
            Time.timeScale = 1f;
        }
        ifPause = !ifPause;
    }

    void toHome()
    {
        //Goto homepage
        Debug.Log("回到主界面");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    void setting()
    {
        //setting the game
        Debug.Log("设置");
        pauseGame();
        mainCamera.enabled=false;
        settingCamera.enabled = true;
        UICanvas.enabled=false;
    }

    public void setIfPause(bool ifPause)
    {
        this.ifPause = ifPause;
    }

}

    

