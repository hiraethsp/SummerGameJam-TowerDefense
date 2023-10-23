using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GroundTower : MonoBehaviour
{
    [SerializeField] float myStatus_MaxHealth = 1000f;//最大生命值
    [SerializeField] float myCurrentHealth;//当前生命值
    [SerializeField] public float myStatus_PhysicalAttack = 5000f; //物理攻击力

    [SerializeField] public float myStatus_MagicAttack = 0f; //法术攻击力

    [SerializeField] public float myStatus_RealAttack = 0f; //真实攻击力（不会受物抗法抗影响）
    [SerializeField] float myStatus_PhysicalDefend = 10f;//物抗
    [SerializeField] float myStatus_MagicDefend = 10f;//法抗
    [SerializeField] public float myStatus_AttackTime = 0.5f; //攻击间隔
    Animator myAnimator;

    public CS_MapCube myMapCube;
    private List<CS_Enemy> enemyList = new List<CS_Enemy>();//阻挡敌人列表
    private float myTimer;//计时器
    public bool isAwake = false;// 用于预览
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        myCurrentHealth = myStatus_MaxHealth;
        myTimer = myStatus_AttackTime;
        myAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAwake) return;
        if (CS_GameManager.Instance.onPause) return;
        Update_Block();
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
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].changeMove();
                //enemyList[i].targetTower = null;
            }
            if (myMapCube != null)
            {
                myMapCube.towerGo = null;
                myMapCube.canBuild = true;
            }
            Dead();
            if (myAnimator != null)
            {
                myAnimator.SetTrigger("Die");
            }
            isAwake = false;
            CS_GameManager.Instance.LoseGroundTower(this);
            myMapCube.DestroyTower();
            Destroy(this.gameObject, 1f);
        }
    }
    private void Update_Block()
    {
        enemyList.Clear();
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        //Debug.Log(enemyList.Count);
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            Vector3 t_position = t_enemyList[i].transform.position;
            if (t_position.x < this.transform.position.x + 1
            && t_position.x > this.transform.position.x - 1
            && t_position.z < this.transform.position.z + 1
            && t_position.z > this.transform.position.z - 1)
            {
                enemyList.Add(t_enemyList[i]);
            }
        }
        if (enemyList.Count == 0) return;
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].getTargetTower(this);
            enemyList[i].changeAttack();
        }
        myTimer -= Time.fixedDeltaTime;
        if (myTimer <= 0)
        {
            if (myAnimator != null) { myAnimator.SetTrigger("Attack"); }
            myTimer = myStatus_AttackTime;
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].getTargetTower(this);
                enemyList[i].takeDamage(myStatus_PhysicalAttack, myStatus_MagicAttack, myStatus_RealAttack);
                enemyList[i].changeAttack();
            }
        }
    }
    public void Dead() { }
}
