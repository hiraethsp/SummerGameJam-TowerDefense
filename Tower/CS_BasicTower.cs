using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BasicTower : MonoBehaviour
{
    [SerializeField] public GameObject myEffectPrefab = null;

    //protected CS_Effect myEffect = null;

    [SerializeField] float myStatus_MaxHealth = 1000f; //最大生命值

    [SerializeField] float myCurrentHealth; //当前生命值

    [SerializeField] public float myStatus_PhysicalAttack = 5000f; //物理攻击力

    [SerializeField] public float myStatus_MagicAttack = 0f; //法术攻击力

    [SerializeField] public float myStatus_RealAttack = 0f; //真实攻击力（不会受物抗法抗影响）

    [SerializeField] public float myStatus_PhysicalDefend = 0f; //物抗

    [SerializeField] public float myStatus_MagicDefend = 0f; //法抗

    [SerializeField] public float myStatus_AttackTime = 0.5f; //攻击间隔

    [SerializeField] public float myStatus_AttackField = 3; //攻击范围，2为一格，因为几何中心原因加1

    [SerializeField] public float myStatus_AttackLoseSpeed = 0f; //减速效果(时间)
    [SerializeField] bool canSputter = false;//是否可以攻击溅射
    [SerializeField] float myStatus_SputteringRange = 2.1f;//溅射范围
    [SerializeField] bool canAttack = true;//是否可以攻击
    public CS_MapCube myMapCube;
    private float myAttackTimer; //攻击间隔
    private CS_Enemy myTargetEnemy = new CS_Enemy(); //攻击目标
    public Animator myAnimator;
    public bool isAwake = false;// 用于预览

    public virtual void Start()
    {
        this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        myCurrentHealth = myStatus_MaxHealth;
        myAttackTimer = myStatus_AttackTime;
        myAnimator = this.gameObject.GetComponent<Animator>();
    }

    public virtual void FixedUpdate()
    {
        if (!isAwake) return;
        if (CS_GameManager.Instance.onPause) return;
        if (!canAttack) return;
        Update_Attack();
    }

    public virtual void Update_Attack()
    {
        myAttackTimer -= Time.fixedDeltaTime;
        if (myAttackTimer > 0)
            return;
        myAttackTimer = myStatus_AttackTime;
        CheckEnemy();
        if (myTargetEnemy == null) return;
        if (myAnimator != null)
            myAnimator.SetTrigger("Attack");
        if (!canSputter)
        {
            myTargetEnemy.takeDamage(myStatus_PhysicalAttack, myStatus_MagicAttack, myStatus_RealAttack);
            myTargetEnemy.loseSpeed(myStatus_AttackLoseSpeed);
        }
        else
        {
            SputteringAttack();
        }
        changeDirection();
    }
    public void CheckEnemy()
    {
        myTargetEnemy = null;
        List<CS_Enemy> enemyList = new List<CS_Enemy>();
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            Vector3 t_position = t_enemyList[i].transform.position;
            if (
                t_position.x <= this.transform.position.x + myStatus_AttackField
                && t_position.x >= this.transform.position.x - myStatus_AttackField
                && t_position.z <= this.transform.position.z + myStatus_AttackField
                && t_position.z >= this.transform.position.z - myStatus_AttackField
            )
            {
                enemyList.Add(t_enemyList[i]);
            }
        }
        if (enemyList.Count == 0)
        {
            return;
        }
        myTargetEnemy = enemyList[0];
        for (int i = 1; i < enemyList.Count; i++) //检查距离终点距离，优先攻击距离终点近的
        {
            if (myTargetEnemy.getWealth() > enemyList[i].getWealth())
            {
                myTargetEnemy = enemyList[i];
            }
        }
    }
    public void changeDirection()
    {
        if (myTargetEnemy == null) return;
        Vector3 t_direction = myTargetEnemy.transform.position - this.transform.position;
        float tanAngleValue2 = 90;
        if (t_direction.z != 0)
        {
            float tanValue2 = Mathf.Abs(t_direction.x) / Mathf.Abs(t_direction.z);//正切值
            float tanRadianValue2 = Mathf.Atan(tanValue2);//求弧度值
            tanAngleValue2 = tanRadianValue2 / Mathf.PI * 180;//求角度
        }
        if (t_direction.x > 0 && t_direction.z < 0) tanAngleValue2 += 90;
        else if (t_direction.x < 0 && t_direction.z < 0) tanAngleValue2 = -tanAngleValue2 - 90;
        else if (t_direction.x < 0 && t_direction.z > 0) tanAngleValue2 = -tanAngleValue2;
        this.transform.rotation = Quaternion.Euler(0.0f, tanAngleValue2, 0.0f);
    }
    public void SputteringAttack()
    {
        if (myTargetEnemy == null) return;
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
        for (int i = 0; i < enemyList.Count; i++)// 范围内都攻击
        {
            enemyList[i].takeDamage(myStatus_PhysicalAttack, myStatus_MagicAttack, myStatus_RealAttack);
            enemyList[i].loseSpeed(myStatus_AttackLoseSpeed);
        }
    }
    public void takeDamage(float physicalDamage, float magicDamage)//受击函数
    {
        physicalDamage -= myStatus_PhysicalDefend;
        if (physicalDamage <= 1) physicalDamage = 1;
        magicDamage *= (1 - myStatus_MagicDefend / 100f);
        myCurrentHealth -= (physicalDamage + magicDamage);
        if (myCurrentHealth <= 0)
        {
            myMapCube.towerGo = null;
            myMapCube.canBuild = true;
            Dead();
            if (myAnimator != null)
                myAnimator.SetTrigger("Die");
            isAwake = false;
            CS_GameManager.Instance.LoseBasicTower(this);
            Destroy(this.gameObject, 1f);
        }
    }
    public void Dead() { }
}
