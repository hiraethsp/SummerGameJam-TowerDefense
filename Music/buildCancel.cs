using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //音源AudioSource相当于播放器，而音效AudioClip相当于磁带
    public AudioSource music;
    public AudioClip build;//建塔的音效
    public AudioClip cancel;//拆塔的音效

    private void Awake()
    {
        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;

        build = Resources.Load<AudioClip>("music/build");
        cancel = Resources.Load<AudioClip>("music/cancel");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))//如果建塔
        {
            //把音源music的音效设置为build
            music.clip = build;
            //播放音效
            music.Play();
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))//如果拆塔
        {
            //把音源music的音效设置为cancel
            music.clip = cancel;
            //播放音效
            music.Play();
        }

    }
}
//24 33需修改什么样情况下触发
