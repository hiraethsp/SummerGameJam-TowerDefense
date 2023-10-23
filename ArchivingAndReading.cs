using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class ArchivingAndReading : MonoBehaviour
{
    //声明instance
    private static ArchivingAndReading instance = null;
    public static ArchivingAndReading Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }



    //使用json来存档
    public Save CreateSave()
    { //创建一个Save对象存储当前游戏数据
        Save save = new Save();
        save.Day = Temp.Day;
        save.Money = Temp.Money;
        save.CurrentLife = Temp.CurrentLife; 
        return save;
    }



    public void saveByJSON()
    {
        Save save = CreateSave();
        //定义字符串filePath保存文件路径信息（就是在Assets中创建的一个文件夹名称为StreamFile,然后系统会给我创建一个byJson.json用于保存游戏信息）
        string filePath = Application.dataPath + "/StreamFile" + "/byJson.json";
        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("存档成功");
    }

    public void saveByJSON(int num)
    {
        Save save = CreateSave();
        //定义字符串filePath保存文件路径信息（就是在Assets中创建的一个文件夹名称为StreamFile,然后系统会给我创建一个byJson.json用于保存游戏信息）
        string filePath = Application.dataPath + "/StreamFile" + "/byJson_"+num+".json";
        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("存档成功");
    }



    public void LoadByJSON()
    {
        string filePath = Application.dataPath + "/StreamFile" + "/byJson.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string  JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            timeManager.Instance.setDay(save.Day);
            CS_GameManager.Instance.setCost(save.Money);
            CS_GameManager.Instance.setMyHealth(save.CurrentLife);
            Time.timeScale = 1f;
            Debug.Log("读档成功");
            Debug.Log(save.Day);
            Debug.Log(save.Money);
            Debug.Log(save.CurrentLife);
            Debug.Log(timeManager.Instance.getDay());
            Debug.Log(CS_GameManager.Instance.myCost());
            Debug.Log(CS_GameManager.Instance.getMyHealth());


        }
        else
        {
            Debug.Log("File Not Found");
        }


    }

    public void LoadByJSON(int num)
    {
        string filePath = Application.dataPath + "/StreamFile" + "/byJson_"+num+".json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            timeManager.Instance.setDay(save.Day);
            CS_GameManager.Instance.setCost(save.Money);
            CS_GameManager.Instance.setMyHealth(save.CurrentLife);
            Time.timeScale = 1f;
            Debug.Log("读档成功");



        }
        else
        {
            Debug.Log("File Not Found");
        }


    }
}
