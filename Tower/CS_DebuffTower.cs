using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_DebuffTower : MonoBehaviour
{
    [SerializeField] float myStatus_MaxHealth = 1000f;//最大生命值
    [SerializeField] float myCurrentHealth;//当前生命值
    [SerializeField] float myStatus_PhysicalDefend = 10f;//物抗
    [SerializeField] float myStatus_MagicDefend = 10f;//法抗
    [SerializeField] float myStatus_DebuffPower = 50f;//减速程度
    [SerializeField] float myStatus_DebuffField = 3;//周围8格，2为一格，中心点原因+1
    private List<CS_Enemy> enemyList=new List<CS_Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        myCurrentHealth = myStatus_MaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Update_Debuff();
    }
    public void takeDamage(float physicalDamage, float magicDamage)//受击函数
    {
        physicalDamage -= myStatus_PhysicalDefend;
        if (physicalDamage <= 1) physicalDamage = 1;
        magicDamage *= (1 - myStatus_MagicDefend / 100);
        myCurrentHealth -= (physicalDamage + magicDamage);
        if (myCurrentHealth <= 0)
        {
            myCurrentHealth = 0;
            this.gameObject.SetActive(false);
        }
    }
    private void Update_Debuff()
    {
        enemyList.Clear();
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        //Debug.Log(enemyList.Count);
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            Vector3 t_position = t_enemyList[i].transform.position;
            if (t_position.x <= this.transform.position.x + myStatus_DebuffField
            && t_position.x >= this.transform.position.x - myStatus_DebuffField
            && t_position.z <= this.transform.position.z + myStatus_DebuffField
            && t_position.z >= this.transform.position.z - myStatus_DebuffField)
            {
                enemyList.Add(t_enemyList[i]);
            }
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].loseSpeed(myStatus_DebuffPower/100);
        }
    }
}
