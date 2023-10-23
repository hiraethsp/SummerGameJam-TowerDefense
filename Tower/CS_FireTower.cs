using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FireTower : MonoBehaviour
{
    [SerializeField] protected GameObject myEffectPrefab = null;
    //protected CS_Effect myEffect = null;

    [SerializeField] float myStatus_MaxHealth = 1000f;//最大生命值
    [SerializeField] float myCurrentHealth;//当前生命值
    [SerializeField] private float myStatus_PhysicalAttack = 5000f;//物理攻击力
    [SerializeField] private float myStatus_MagicAttack = 0f;//法术攻击力
    [SerializeField] private float myStatus_RealAttack = 0f;//真实攻击力（不会受物抗法抗影响）
    [SerializeField] float myStatus_PhysicalDefend = 0f;//物抗
    [SerializeField] float myStatus_MagicDefend = 0f;//法抗
    [SerializeField] private float myStatus_AttackTime = 0.5f;//攻击间隔
    [SerializeField] private float myStatus_AttackField = 3;//攻击范围，2为一格，因为几何中心原因加1
    [SerializeField] float myStatus_SputteringRange = 2.1f;//溅射范围
    private float myAttackTimer;//攻击间隔
    private CS_Enemy myTargetEnemy = new CS_Enemy();//攻击目标
    void Start()
    {
        myCurrentHealth = myStatus_MaxHealth;
        myAttackTimer = myStatus_AttackTime;
    }

    void FixedUpdate()
    {
        Update_Attack();
    }
    private void Update_Attack()
    {
        myAttackTimer -= Time.fixedDeltaTime;
        if (myAttackTimer > 0) return;
        CheckEnemy();
        if (myTargetEnemy == null)
        {
            myAttackTimer = 0;
            return;
        }
        SputteringAttack();
        myAttackTimer = myStatus_AttackTime;
    }
    private void CheckEnemy()
    {
        myTargetEnemy = null;
        List<CS_Enemy> enemyList = new List<CS_Enemy>();
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            Vector3 t_position = t_enemyList[i].transform.position;
            if (t_position.x <= this.transform.position.x + myStatus_AttackField
            && t_position.x >= this.transform.position.x - myStatus_AttackField
            && t_position.z <= this.transform.position.z + myStatus_AttackField
            && t_position.z >= this.transform.position.z - myStatus_AttackField)
            {
                enemyList.Add(t_enemyList[i]);
            }
        }
        if (enemyList.Count == 0)
        {
            return;
        }
        myTargetEnemy = enemyList[0];
        for (int i = 1; i < enemyList.Count; i++)//检查距离终点距离，优先攻击距离终点近的
        {
            if (myTargetEnemy.getWealth() > enemyList[i].getWealth())
            {
                myTargetEnemy = enemyList[i];
            }
        }
    }
    private void SputteringAttack()
    {
        Debug.Log("攻击一次");
        List<CS_Enemy> enemyList = new List<CS_Enemy>();
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            Vector3 t_position = t_enemyList[i].transform.position;
            if (t_position.x <= myTargetEnemy.transform.position.x + myStatus_SputteringRange
            && t_position.x >= myTargetEnemy.transform.position.x - myStatus_SputteringRange
            && t_position.z <= myTargetEnemy.transform.position.z + myStatus_SputteringRange
            && t_position.z >= myTargetEnemy.transform.position.z - myStatus_SputteringRange)
            {
                enemyList.Add(t_enemyList[i]);
            }
        }
        Debug.Log(myTargetEnemy.transform.position.x + " " + myTargetEnemy.transform.position.z);
        for (int i = 0; i < enemyList.Count; i++)// 范围内都攻击
        {
            enemyList[i].takeDamage(myStatus_PhysicalAttack, myStatus_MagicAttack, myStatus_RealAttack);
        }
    }
}

