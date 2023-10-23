using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    private static timeManager instance = null;
    public static timeManager Instance { get { return instance; } }
    private int day = 1;
    [SerializeField] float dayTime = 360f;//时间标准
    [SerializeField] Material skyBoxDay;
    [SerializeField] Material skyBoxNight;
    [SerializeField] private float timer = 0f;//计时器
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        //day = 3;
        //从存档读取天数
    }
    void Update()
    {
        if (CS_GameManager.Instance.onPause) return;
        // 如果开始计时，累加计时器
        timer += Time.deltaTime;
        // 如果计时器超过了一天的时长，重置为0，天数加一
        if (timer >= dayTime)
        {
            timer = 0f;
            day++;
        }
    }
    public float getTime()
    {
        return timer;
    }
    public int getDay()
    {
        return day;
    }
    public void setTime(float timer)
    {
        this.timer=timer;
    }
    public void setDay(int day)
    {
        this.day=day;
    }
}
