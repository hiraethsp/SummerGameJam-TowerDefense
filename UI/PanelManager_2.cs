using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelManager2 : MonoBehaviour
{
    private static PanelManager2 instance = null;
    public static PanelManager2 Instance { get { return instance; } }
    [SerializeField]public GameObject[] panel;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    public void openPanel(int panelindex){
        panel[panelindex].SetActive(true);
}
    public void closePanel(int panelindex){
        panel[panelindex].SetActive(false);
    }
}
