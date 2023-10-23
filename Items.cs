using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Items : MonoBehaviour
{
    Button Item_1_Btn;
    Button Item_2_Btn;
    Button Item_3_Btn;


    void Awake()
    {
        Item_1_Btn = transform.GetChild(0).GetComponent<Button>();
        Item_2_Btn = transform.GetChild(1).GetComponent<Button>();
        Item_3_Btn = transform.GetChild(2).GetComponent<Button>();


        Item_1_Btn.onClick.AddListener(Item_1_use);
        Item_2_Btn.onClick.AddListener(Item_2_use);
        Item_3_Btn.onClick.AddListener(Item_3_use);
    }

    void Item_1_use()
    {
        //Show other buttons
        Debug.Log("使用道具1");


    }
    void Item_2_use()
    {
        //Restart game
        Debug.Log("使用道具2");
        
    }
    void Item_3_use()
    {
        //Goto homepage
        Debug.Log("使用道具3");

    }

}

