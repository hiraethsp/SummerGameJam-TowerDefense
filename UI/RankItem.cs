using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankItem : MonoBehaviour
{
    [Header("Child")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;



    /// <summary>
    /// Pass the parameters in will change the element of RankItem.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public void Initiate(string name, float score)
    {
        nameText.text = name;
        scoreText.text = score.ToString();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
