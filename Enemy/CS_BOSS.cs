using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BOSS : CS_Enemy
{
    //private static CS_BOSS instance = null;
    [SerializeField] GameObject myEffectPrefab;
    //public static CS_BOSS Instance { get { return instance; } }
    [SerializeField] public float BossSkillTime = 3f;//Boss技能
    [SerializeField] GameObject ChickenPrefab;
    [SerializeField] GameObject EggPrefab;
    private float BossTimer;//boss技能计时器
    // private void Awake()
    // {
    //     if (instance != null && instance != this)
    //     {
    //         Destroy(this.gameObject);
    //     }
    //     else
    //     {
    //         instance = this;
    //     }
    // }
    void Start()
    {
        BossTimer = BossSkillTime;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Health();
        //Debug.Log(BossTimer);
        BossTimer -= Time.fixedDeltaTime;
        if (BossTimer > 0) return;
        if (myEffectPrefab != null)
        {
            Vector3 effectPosition = this.transform.position;
            effectPosition.y += 5f;
            GameObject effect = GameObject.Instantiate(myEffectPrefab, effectPosition, Quaternion.identity);
            Destroy(effect, 1f);
        }
        if (myAnimator != null)
            myAnimator.SetTrigger("Skill");
        BossTimer = BossSkillTime;
        List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
        for (int i = 0; i < t_enemyList.Count; i++)
        {
            AChick test = t_enemyList[i].gameObject.GetComponent<AChick>();
            if (test == null)
            {
                t_enemyList[i].myBirthPrefab = ChickenPrefab;
            }
        }
    }
    public override void BossSkill()
    {
        GameObject t_enemyObject = Instantiate(EggPrefab);
        CS_Enemy t_enemy = t_enemyObject.GetComponent<CS_Enemy>();
        t_enemy.Init(this.transform.position, myCurrentPoint);
        CS_GameManager.Instance.addMyEnemyList(t_enemy);
    }
    public override void Dead()
    {
        CS_GameManager.Instance.myWin();
    }
    public void Health()
    {
        //Debug.Log(myCurrentHealth / myStatus_MaxHealth);
        if (CS_Health.Instance != null)
        {
            CS_Health.Instance.gameObject.SetActive(true);
            if (CS_HealthSlider.Instance != null && CS_HealthSlider.Instance.t_image != null)
                CS_HealthSlider.Instance.t_image.fillAmount = myCurrentHealth / myStatus_MaxHealth;
        }
    }
}
