using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HealthSlider : MonoBehaviour
{
    private static CS_HealthSlider instance = null;
    public static CS_HealthSlider Instance { get { return instance; } }
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
        t_image = this.gameObject.GetComponent<Image>();
    }
    void FixedUpdate()
    {

    }
}
