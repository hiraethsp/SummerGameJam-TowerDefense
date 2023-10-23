using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    Button sound_ON;
    Button sound_OFF;

    void Awake()
    {
        sound_ON = transform.GetChild(0).GetComponent<Button>();
        sound_OFF = transform.GetChild(1).GetComponent<Button>();
        //GameObject.Find("rank").GetComponent<Button>();

        sound_OFF.gameObject.SetActive(false);

        sound_ON.onClick.AddListener(turnOffSound);
        sound_OFF.onClick.AddListener(turnOnSound);

    }


    
    void turnOffSound()
    {
        //此处应先调用音量控制脚本中的函数调零音量，现略

        sound_ON.gameObject.SetActive(false);
        sound_OFF.gameObject.SetActive(true);
    }




   
    void turnOnSound()
    {
        //此处应先调用音量控制脚本中的函数开启音量，现略

        sound_ON.gameObject.SetActive(true);
        sound_OFF.gameObject.SetActive(false);

    }
}
