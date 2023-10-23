using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyArray;//敌人
    [SerializeField] int point;//数据地图坐标

    [System.Serializable]
    public class enemyBirth
    {
        [SerializeField] public int timer;
        [SerializeField] public int type;
    }
    private int arrayCount = 0;//记录数组到第几个了
    [SerializeField] enemyBirth[] myEnemyBirth;

    // Update is called once per frame
    void Update()
    {
        Update_Timer();
    }
    void Update_Timer()
    {
        if (arrayCount >= myEnemyBirth.Length) return;
        float timer = timeManager.Instance.getTime();
        if (timer < myEnemyBirth[arrayCount].timer) return;
        GameObject t_enemyObject = Instantiate(enemyArray[myEnemyBirth[arrayCount].type]);
        arrayCount++;
        CS_Enemy t_enemy = t_enemyObject.GetComponent<CS_Enemy>();
        t_enemy.Init(this.transform.position, point);
        CS_GameManager.Instance.addMyEnemyList(t_enemy);
    }
}
;