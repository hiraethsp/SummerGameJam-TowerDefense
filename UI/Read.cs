using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using TMPro;


public class Read : MonoBehaviour
{
    //GameObject story_1;
    //GameObject story_2;
    //GameObject story_3;
    Save save;

    Button story_1_Btn;
    Button story_2_Btn;
    Button story_3_Btn;


    TextMeshProUGUI text_1_name;
    TextMeshProUGUI text_1_day;
    TextMeshProUGUI text_2_name;
    TextMeshProUGUI text_2_day;
    TextMeshProUGUI text_3_name;
    TextMeshProUGUI text_3_day;

    void Awake()
    {

        story_1_Btn = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        story_2_Btn = transform.GetChild(1).GetChild(1).GetComponent<Button>();
        story_3_Btn = transform.GetChild(2).GetChild(1).GetComponent<Button>();
        //GameObject.Find("rank").GetComponent<Button>();

        text_1_name = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        text_1_day = transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();

        text_2_name = transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        text_2_day = transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>();

        text_3_name = transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
        text_3_day = transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>();

        story_1_Btn.onClick.AddListener(Read_1);
        story_2_Btn.onClick.AddListener(Read_2);
        story_3_Btn.onClick.AddListener(Read_3);
    }

    void Update()
    {
        textSet();
    }

    void Read_1()
    {
        LoadByJSON(1);
    }

    void Read_2()
    {
        LoadByJSON(2);
    }

    void Read_3()
    {
        LoadByJSON(3);
    }





    private void textSet()
    {
        string filePath_1 = Application.dataPath + "/StreamFile" + "/byJson_1.json";
        if (File.Exists(filePath_1))
        {
            StreamReader sr = new StreamReader(filePath_1);
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            text_1_day.text= "Day:" + "  " + save.Day.ToString();
            text_1_name.text = "Name:" + "  " + save.Name;
            Time.timeScale = 1f;

        }
        else
        {
            Debug.Log("File Not Found");
        }
        
        string filePath_2 = Application.dataPath + "/StreamFile" + "/byJson_2.json";
        if (File.Exists(filePath_2))
        {
            StreamReader sr = new StreamReader(filePath_2);
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            text_2_day.text= "Day:" + "  " + save.Day.ToString();
            text_2_name.text = "Name:" + "  " + save.Name;
            Time.timeScale = 1f;

        }
        else
        {
            Debug.Log("File Not Found");
        }
        
        string filePath_3 = Application.dataPath + "/StreamFile" + "/byJson_3.json";
        if (File.Exists(filePath_3))
        {
            StreamReader sr = new StreamReader(filePath_3);
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            text_3_day.text= "Day:" + "  " + save.Day.ToString();
            text_3_name.text = "Name:" + "  " + save.Name;
            Time.timeScale = 1f;
        }
        else
        {
            Debug.Log("File Not Found");
        }
    }



    public void LoadByJSON(int num)
    {
        string filePath = Application.dataPath + "/StreamFile" + "/byJson_" + num + ".json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string JsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(JsonString);
            Temp.Day = save.Day;
            Temp.CurrentLife = save.CurrentLife;
            Temp.Money = save.Money;
            Temp.playerName = save.Name;
            if (save.Day == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            }else if (save.Day == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            }else if(save.Day == 3)
            {
               UnityEngine.SceneManagement.SceneManager.LoadScene("Final");
            }
            
            Time.timeScale = 1f;
            Debug.Log("读档成功");
            // Debug.Log(save.Day);
            //Debug.Log(save.Money);
            //Debug.Log(save.CurrentLife);
            //Debug.Log(timeManager.Instance.getDay());
            //Debug.Log(CS_GameManager.Instance.myCost());
            //Debug.Log(CS_GameManager.Instance.getMyHealth());
            //Debug.Log(Temp.Day);
            //Debug.Log(Temp.Money);


        }
        else
        {
            Debug.Log("File Not Found");
        }


    }






}
