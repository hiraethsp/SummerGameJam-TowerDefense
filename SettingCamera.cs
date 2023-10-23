using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCamera : MonoBehaviour
{
    Camera mainCamera;
    Camera settingCamera;


    void Awake()
    {
       
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        settingCamera = GameObject.Find("Setting Camera").GetComponent<Camera>();

        //GameObject.Find("rank").GetComponent<Button>();


    }



    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        settingCamera.enabled = false;

        Time.timeScale = 1f;
        Pause.Instance.setIfPause(false);
    }


}
