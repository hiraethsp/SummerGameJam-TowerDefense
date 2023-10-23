using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Health : MonoBehaviour
{
    private static CS_Health instance = null;
    public static CS_Health Instance { get { return instance; } }
    public Image t_image;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        this.gameObject.SetActive(false);
    }
}
