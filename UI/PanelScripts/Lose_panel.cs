using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Lose_panel : MonoBehaviour
{
    [SerializeField]Button MainmenuBtn;
    void Start(){
        MainmenuBtn.onClick.AddListener(back);
    }
    void back(){
        SceneManager.LoadScene("Mainmenu");
    }
}
