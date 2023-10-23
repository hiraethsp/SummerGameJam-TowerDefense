using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

//DLC Rougelikemode
public class itemSystem : MonoBehaviour
{
    [SerializeField]public Button saveBtn;
    // [SerializeField]public Button awardBtn;
    [SerializeField]public Button nextBtn;
    private string filePath;
    RankGameManager rankGameManager;
    RankManager rankManager;
    float score = 10;


    int day;
    void Start()
    {

        //day =timeManager.Instance.getDay();
        day = Temp.Day;
        saveBtn.onClick.AddListener(save);
        // awardBtn.onClick.AddListener(award);
        nextBtn.onClick.AddListener(next);
        Debug.Log(Temp.Day);
        Debug.Log(day);
    }
    void save(){
        //写存档(一开始点的几号存档就存在几号位)
        saveByJSON(Temp.storyNum);
        SceneManager.LoadScene("MainMenu");
    }
    // void award(){
    //     switch(day){
    //         case 1:everydayitem();rareitem();break;
    //         case 2:everydayitem();rareitem();epicitem();break;
    //         case 3:Debug.Log("没有奖励");break;
    //     }
    // }
    void next(){
        if(day==1){
            Temp.Day = 2;
            saveByJSON(Temp.storyNum);
            Debug.Log(Temp.Day);
            SceneManager.LoadScene("Level2");
            Time.timeScale = 1f;
            timeManager.Instance.setDay(2);
        }else if(day==2){
            Temp.Day = 3;
            saveByJSON(Temp.storyNum);
            SceneManager.LoadScene("Final");
            Time.timeScale = 1f;
            timeManager.Instance.setDay(3);

        }
        else if(day==3){
            Debug.Log("你赢了");
            //先摧毁这个存档就是重置为0，然后帮我补充排行功能
            //删除存档文件
            deleteStory(Temp.storyNum);
            //调用方法存姓名以及分数
            //当打开排行榜界面时生成排行榜（相应的函数写在Rank中）
            beforeRank();
            Debug.Log("Temp.storyNum");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Rank");

       }
    }

    private void deleteStory(int num)
    {
        filePath = Application.dataPath + "/StreamFile" + "/byJson_" + num + ".json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File deleted: " + filePath);
        }
        else
        {
            Debug.Log("File does not exist: " + filePath);
        }
    }

    private void beforeRank()
    {
        rankGameManager = RankGameManager.instance;
        Debug.Log(rankGameManager);
        rankGameManager.m_PlayerName = Temp.playerName;
        rankGameManager.m_Score = Temp.Score;
        rankGameManager.SaveInfo();
        Debug.Log("准备排行成功");
        Debug.Log(score);
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












    //     void everydayitem(){
    //     rareitem();
    //     for(int i=1;i<day*2;i++){
    //     normalitem();
    //         }
    //     debuffitem();
    //     }
    // //-----抽取环节-----
    // public int rareitem(){
    //     int rareindex=Random.Range(1,11);//随机抽取道具从十种rare道具中抽取
    //     Debug.Log("抽到"+rareindex);
    //     return rareindex;
    // }

    // public int normalitem(){
    //     int normalindex=Random.Range(1,6);//从normal道具中抽,抽取次数为一个函数
    //     Debug.Log("抽到"+normalindex);
    //     return normalindex;
    // }

    // public int epicitem(){
    //     int epicindex=Random.Range(1,7);
    //     Debug.Log("epic"+epicindex);
    //     return epicindex;
    // }


    // public int debuffitem(){
    //     int debuffindex=Random.Range(1,6);
    //     Debug.Log("debuff"+debuffindex);
    //     return debuffindex;
    // }
}

