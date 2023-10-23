using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankGameManager : MonoBehaviour
{
    public static RankGameManager instance;

    [Header("Parameters")]
    public float m_Score = 0f;
    public bool m_GameOver = false;
    public string m_PlayerName = null;


    private void Awake()
    {
        // 在 Awake 方法中设置 instance 为当前组件的实例
        instance = this;
    }



    public class Rank_Item
    {
        public string name;
        public float score;
    }
    private List<Rank_Item> rank_items = new List<Rank_Item>();

    /// <summary>
    /// To limit the visible number of RankItem.
    /// </summary>
    private int rankLimit = 10;

    /// <summary>
    /// Parse the information about rank from locating strings
    /// </summary>
    public void ParseInfo()
    {
        rank_items = new List<Rank_Item>();
        string rankItemString = PlayerPrefs.GetString("PONGPI_SaveInfo");
        if (!string.IsNullOrEmpty(rankItemString))
        {
            string[] rankItemArry=rankItemString.Split('|');
            for(int ident = 0; ident < rankItemArry.Length; ident++)
            {
                string[] rankItem = rankItemArry[ident].Split('+');
                Rank_Item ri=new Rank_Item();

                ri.name= rankItem[0];
                try
                {
                    ri.score = float.Parse(rankItem[1]);
                }
                catch { }
                rank_items.Add(ri);

            }
        }
        //比较大小
        if(rank_items.Count != 0)
        {
            rank_items.Sort(CompareTime);
        }


    }

    public void SaveInfo()
    {
        ParseInfo();
        Rank_Item ri = new Rank_Item();
        ri.name = m_PlayerName;
        ri.score = m_Score;
        rank_items.Add(ri);
        rank_items.Sort(CompareTime);
        //限制排行榜显示名次
        if(rank_items.Count > rankLimit)
        {
            rank_items.RemoveAt(rank_items.Count - 1);
        }
        //将数据转换为字符串存储于注册表里面
        string rankItemString = "";
        for(int ident = 0; ident < rank_items.Count; ident++)
        {
            string temp = "";
            temp+= rank_items[ident].name;
            temp += "+";
            temp+=rank_items[ident].score;


            if(ident!= rank_items.Count - 1)
            {
                temp += "|";
            }
            rankItemString += temp;
        }
        PlayerPrefs.SetString("PONGPI_SaveInfo", rankItemString);
        
    }


    /// <summary>
    /// Using to get the information on the rank list which had been saved in the registry.
    /// </summary>
    /// <returns>Save List</returns>
    public List<Rank_Item> GetInfo()
    {
        ParseInfo();
        return rank_items;
    }


        /// <summary>
        /// Compare two objects based on time in the Sort() method.
        /// </summary>
        /// <param name="player_1"></param>
        /// <param name="player_2"></param>
        /// <returns></returns>
        int CompareTime(Rank_Item player_1, Rank_Item player_2)
        {
            float score_1 = player_1.score;
            float score_2 = player_2.score;
            if (score_1 == score_2 )
            {
                return 0;
            }
            else if (score_1 > score_2 )
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    






}
