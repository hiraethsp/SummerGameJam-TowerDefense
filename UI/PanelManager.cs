using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelManager : MonoBehaviour
{
    private static PanelManager instance = null;
    public static PanelManager Instance { get { return instance; } }
    public GameObject[] panel;
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
