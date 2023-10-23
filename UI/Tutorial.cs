using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField]public Button tipBtn;
    [SerializeField]public Button bookBtn;
    [SerializeField]public Button backBtn;
    [SerializeField]public Button finalDemoBtn;
    void Awake(){
        tipBtn.onClick.AddListener(tips);
        bookBtn.onClick.AddListener(book);
        backBtn.onClick.AddListener(back);
        finalDemoBtn.onClick.AddListener(final);
    }
    void tips(){
        PanelManager.Instance.openPanel(6);
        PanelManager.Instance.closePanel(5);
    }
    void book(){
        Application.OpenURL("https://plastichub.unity.cn/whuse-project/My-project-3D");
    }
    void back(){
        PanelManager.Instance.openPanel(1);
        PanelManager.Instance.closePanel(5);
    }
    void final(){
        SceneManager.LoadScene("FinalDemo");
    }
}
