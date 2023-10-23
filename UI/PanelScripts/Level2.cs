// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class Level2 : MonoBehaviour
// {
//    [SerializeField]public Button saveBtn;
//     // [SerializeField]public Button awardBtn;
//     [SerializeField]public Button nextBtn;
//     int day;
//     void Start()
//     {
//         day=timeManager.Instance.getDay();
//         saveBtn.onClick.AddListener(save);
//         // awardBtn.onClick.AddListener(award);
//         nextBtn.onClick.AddListener(next);
//     }
//     void save(){
//         //写存档
//         SceneManager.LoadScene("MainMenu");
//     }
//     void next(){
//         if(day==2){
//             SceneManager.LoadScene("Final");
//             //先摧毁这个存档就是重置为0，然后帮我补充排行功能
//         }
//     }
// }
