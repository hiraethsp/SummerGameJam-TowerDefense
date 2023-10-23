using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    private static Temp instance;
    public static int Day=1;
    public static int CurrentLife=20;
    public static float Money=10000;
    public static int storyNum;
    public static int Score;
    public static string playerName;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Final" && mode == LoadSceneMode.Single)
        {
            // 在Final加载完成后执行逻辑
            Debug.Log(Day);
            Day = 3;
            Debug.Log(CurrentLife);
            Judge();

        }else if (scene.name == "Level1" && mode == LoadSceneMode.Single)
        {
            Day = 1;
            Judge();
        }else if(scene.name == "Level2" && mode == LoadSceneMode.Single)
        {
            Day = 2;
            Judge();
        }
    }

    private void Judge()
    {
            timeManager.Instance.setDay(Day);
            CS_GameManager.Instance.setCost(Money);
            CS_GameManager.Instance.setMyHealth(CurrentLife);
    }




    private void Update()
    {
        if (timeManager.Instance == null)
        {
            return;
        }
        else 
        {
            Day = timeManager.Instance.getDay();
            Money = CS_GameManager.Instance.myCost();
            CurrentLife = CS_GameManager.Instance.getMyHealth();
            Score =(int) Money / 500 + CurrentLife;
        }
      
        
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}