using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ItemOnce : CS_BasicTower
{
    [SerializeField] float myStatus_EffectField;//生效范围
    public override void Start()//范围内造成伤害
    {
        if (myEffectPrefab != null)
        {
            Vector3 effectPosition = this.transform.position;
            effectPosition.y += 2.5f;//中心向上移动0.25以保证在cube上面
            GameObject effect = GameObject.Instantiate(myEffectPrefab, effectPosition, Quaternion.identity);
            //this.transform.rotation = Quaternion.Euler(-90f, 0f, 0.0f);
            Destroy(effect, 2f);
        }
        List<CS_Enemy> enemyList = new List<CS_Enemy>();
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            Vector3 t_position = t_enemyList[i].transform.position;
            if (t_position.x <= this.transform.position.x + myStatus_EffectField
            && t_position.x >= this.transform.position.x - myStatus_EffectField
            && t_position.z <= this.transform.position.z + myStatus_EffectField
            && t_position.z >= this.transform.position.z - myStatus_EffectField)
            {
                enemyList.Add(t_enemyList[i]);
            }
        }
        for (int i = 0; i < enemyList.Count; i++)// 范围内都攻击
        {
            enemyList[i].takeDamage(myStatus_PhysicalAttack, myStatus_MagicAttack, myStatus_RealAttack);
            enemyList[i].loseSpeed(myStatus_AttackLoseSpeed);
        }
        Destroy(this.gameObject);
    }
}