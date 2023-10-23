using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotoMainMenu : MonoBehaviour
{
    public Button ExitBtn;
    void Awake()
    {
        ExitBtn=transform.GetChild(0).GetComponent<Button>();
        ExitBtn.onClick.AddListener(exit);
    }

    void exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }




}
