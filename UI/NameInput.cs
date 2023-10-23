using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class NameInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;
    public string playerName;
    Save save;
    void Start()
    {
        submitButton.onClick.AddListener(SubmitName);
    }
    void SubmitName()
    {
        if(playerName!=null){
        playerName = inputField.text;
            Temp.playerName = playerName;
        Debug.Log("Player Name: " + playerName);
            saveByJSON(Temp.storyNum);
            save = CreateSave();
            Debug.Log(Temp.storyNum);
            /*if (Temp.storyNum == 1)
            {
                text_1_day.text = "Day:" + "  " + save.Day.ToString();
                text_1_name.text = "Name:" + "  " + save.Name.ToString();

            }else if (Temp.storyNum == 2)
            {
                text_2_day.text = "Day:" + "  " + save.Day.ToString();
                text_2_name.text = "Name:" + "  " + save.Name.ToString();
            }else if (Temp.storyNum == 3)
            {
                text_3_day.text = "Day:" + "  " + save.Day.ToString();
                text_3_name.text = "Name:" + "  " + save.Name.ToString();
            }*/
            LoadByJSON(Temp.storyNum);
        }
    }








    //使用json来存档
    public Save CreateSave()
    { //创建一个Save对象存储当前游戏数据
        Save save = new Save();
        save.Day = Temp.Day;
        save.Name = Temp.playerName;
        save.Money = Temp.Money;
        save.CurrentLife = Temp.CurrentLife;
        return save;
    }


    public void saveByJSON(int num)
    {
        Save save = CreateSave();
        //定义字符串filePath保存文件路径信息（就是在Assets中创建的一个文件夹名称为StreamFile,然后系统会给我创建一个byJson.json用于保存游戏信息）
        string filePath = Application.dataPath + "/StreamFile" + "/byJson_" + num + ".json";
        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("存档成功");
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
            if (Temp.Day == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            }
            else if (Temp.Day == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            }
            else if (Temp.Day == 3)
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