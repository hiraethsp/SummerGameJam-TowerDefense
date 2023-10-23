using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    private ArchivingAndReading ar;
    [SerializeField] public Button Btn;


    // Start is called before the first frame update
    void Start()
    {
        Btn=transform.GetChild(0).GetComponent<Button>();
        ar= ArchivingAndReading.Instance;
        Debug.Log(ar);
        Btn.onClick.AddListener(next);
    }

    // Update is called once per frame
    void next()
    {
        ar = ArchivingAndReading.Instance;
        Debug.Log(ar);
    }
}
