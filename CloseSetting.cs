using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseSetting : MonoBehaviour
{
    private static CloseSetting instance = null;
    public static CloseSetting Instance { get { return instance; } }

    Button closeBtn;
    Camera mainCamera;
    Camera settingCamera;
    Canvas UICanvas;
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
        closeBtn = GameObject.Find("CloseButton").GetComponent<Button>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        settingCamera = GameObject.Find("Setting Camera").GetComponent<Camera>();
        UICanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        //GameObject.Find("rank").GetComponent<Button>();

        closeBtn.onClick.AddListener(closeSetting);

    }



    
    public void closeSetting()
    {
        Debug.Log("关闭设置界面");
        mainCamera.enabled = true;
        settingCamera.enabled = false;
        UICanvas.enabled = true;
        Time.timeScale = 1f;
        Pause.Instance.setIfPause(false);
    }

  
}
