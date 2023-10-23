 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 public class RankManager : MonoBehaviour
 {
     [Header("RankList")]
     private RankItem rankItemTemplate;
     public Transform Grid;
     private List<RankItem> rankItem_List = new();

     public static RankManager instance;



     private void Awake()
    {
         // 在 Awake 方法中设置 instance 为当前组件的实例
         instance = this;
     }

     // Start is called before the first frame update
     public void Start()
     {

        rankItemTemplate = transform.GetChild(0).GetComponent<RankItem>();
         Grid= transform.GetChild(1).GetComponent<RectTransform>();
    }




     /// <summary>
     /// Creat a new rankItem and set it in Grid.
     /// </summary>
     private RankItem CreateNewRankItem()
     {
        //Duplicate a rank item by rankItemTemplate.

        GameObject gameObject = Instantiate(rankItemTemplate.gameObject);
         //Get its scripts.
         RankItem newRankItem = gameObject.GetComponent<RankItem>();

         //Set parent and to be active in the Canvas.

         newRankItem.transform.SetParent(Grid);
         newRankItem.gameObject.SetActive(true);
         newRankItem.transform.localScale = Vector3.one;

         rankItem_List.Add(newRankItem);
         Debug.Log(newRankItem);
         return newRankItem;

     }




     /// <summary>
     /// Get the rank at the registry.Or load the rank.
     /// </summary>
     public void Initiate()
    {
         //First , set all of the items to be inactive.

         //To definite the change of the rank information at last open time.
        //It is not must needed if you not need to limit the visible rank.
         if (rankItem_List.Count > 0)
         {
             foreach (RankItem temp in rankItem_List)
             {
                temp.gameObject.SetActive(false);
            }

         }

         List<RankGameManager.Rank_Item> rank_items = RankGameManager.instance.GetInfo();



         if (rank_items.Count <= 0)
        {
             return;
         }
         else
         {
             for (int ident = 0; ident < rank_items.Count; ident++)
             {
                 RankItem rankItem = CreateNewRankItem();
                 rankItem.Initiate(rank_items[ident].name, rank_items[ident].score);
            }
     }




     }
 }
