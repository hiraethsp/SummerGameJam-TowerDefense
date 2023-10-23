using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy : MonoBehaviour
{
    public enum State
    {
        Move,
        Attack,
        Dead,
    }
    [SerializeField] public Animator myAnimator;
    public State myState;
    [SerializeField] float myStatus_MoveSpeed = 5f;//常规移动速度
    [SerializeField] float myStatus_DebuffMoveSpeed = 2.5f;//减速后移动速度
    [SerializeField] public float myStatus_MaxHealth = 1000f;//生命上限
    [SerializeField] float myStatus_AttackTime = 1f;//攻速
    [SerializeField] float myStatus_PhysicalAttack = 100f;//物理攻击力
    [SerializeField] float myStatus_MagicAttack = 0f;//法术攻击力
    [SerializeField] float myStatus_PhysicalDefend = 10f;//物抗
    [SerializeField] float myStatus_MagicDefend = 10f;//法抗
    [SerializeField] int reduceHealthPower = 1;//扣血倍率
    [SerializeField] bool canBarrier = true;//是否可被阻挡
    [SerializeField] int myStatus_Shield = 0;//次数盾
    [SerializeField] public GameObject myBirthPrefab = null;//死亡后生成新的怪
    [SerializeField] float myStatus_Recover = 0f;//自回血
    private int myShieldNumber;
    public CS_GroundTower targetTower;//当前攻击的塔

    public float myCurrentHealth;//当前生命值
    private float attackTimer;//攻击计时器
    private float myCurrentMoveSpeed = 5f;//当前移动速度
    private float loseSpeedTimer;//减速计时器
    private int[,] maps;//地图数据
    private int[,] nextPoint;//路径下一点
    public int myCurrentPoint;//数据地图当前位置
    private int myBirthPoint;//数据地图初始位置
    private int mapWidth;//地图宽度(x轴)
    private int mapHeight;//地图高度(y轴)
    private int[,] wealth;//距离终点距离
    private Vector3 myBirthPosition;//初始位置
    //private bool atBarrier;//是否遇到障碍物
    public void Init(Vector3 position, int point)
    {
        this.transform.position = position;
        myState = State.Move;
        myAnimator = this.gameObject.GetComponent<Animator>();
        myCurrentHealth = myStatus_MaxHealth;
        myCurrentPoint = point;//后面要修改为从CS_EnemyManager中获取(已修改)
        myBirthPoint = myCurrentPoint;
        myShieldNumber = myStatus_Shield;
        myBirthPosition = this.transform.localPosition;
        mapHeight = CS_GameManager.Instance.mapHeight;
        mapWidth = CS_GameManager.Instance.mapWidth;
        //Debug.Log(mapHeight + " " + mapWidth);
        wealth = new int[mapHeight, mapWidth];
        nextPoint = new int[mapHeight, mapWidth];
        myCurrentMoveSpeed = myStatus_MoveSpeed;
        this.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    // void Start()//实际上不用Start，在EnemyManager中调用init。
    // {
    //     myState = State.Move;
    //     myCurrentHealth = myStatus_MaxHealth;
    //     myCurrentPoint = 1001;//后面要修改为从CS_EnemyManager中获取
    //     myBirthPoint = myCurrentPoint;
    //     myBirthPosition = this.transform.localPosition;
    //     mapHeight = CS_GameManager.Instance.mapHeight;
    //     mapWidth = CS_GameManager.Instance.mapWidth;
    //     myMoveSpeed=1;
    // }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (CS_GameManager.Instance.onPause) return;
        Update_Recover();
        Update_ChangeSpeed();
        dijkstra();//当地图修改时，重新规划路线
        //Debug.Log(myState);
        if (myState == State.Move)
        {
            Update_Move();//移动
        }
        else if (myState == State.Attack)
        {
            Update_Attack();//攻击
        }
    }

    void dijkstra()
    {
        maps = CS_GameManager.Instance.maps;
        int point = CS_GameManager.Instance.myPoint;
        wealth = new int[mapHeight, mapWidth];
        nextPoint = new int[mapHeight, mapWidth];
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                wealth[i, j] = 100;
            }
        }
        if (mapHeight == 0) return;
        wealth[point / 100, point % 100] = 0;
        nextPoint[point / 100, point % 100] = -1;
        int[] pointX = new int[4] { -1, 1, 0, 0 };
        int[] pointY = new int[4] { 0, 0, 1, -1 };
        int[,] pointFlag = new int[mapHeight, mapWidth];
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                pointFlag[i, j] = 0;
            }
        }
        while (point != myCurrentPoint)
        {
            int min = 99;
            int k = -1;
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (pointFlag[i, j] == 0 && wealth[i, j] < min && (maps[i, j] == 2 || maps[i, j] == 0))
                    {
                        k = i * 100 + j;
                        min = wealth[i, j];
                    }
                }
            }
            if (k == -1) break;//说明从当前位置无法到达目标点
            for (int i = 0; i <= 3; i++)
            {
                if (k / 100 + pointX[i] >= 0 && k % 100 + pointY[i] >= 0
                && k / 100 + pointX[i] < mapHeight && k % 100 + pointY[i] < mapWidth)
                {
                    if (wealth[k / 100 + pointX[i], k % 100 + pointY[i]] > min + 1)
                    {
                        if (maps[k / 100 + pointX[i], k % 100 + pointY[i]] == 2)
                        {
                            wealth[k / 100 + pointX[i], k % 100 + pointY[i]] = min + 1;
                            nextPoint[k / 100 + pointX[i], k % 100 + pointY[i]] = k;
                        }
                    }
                }
            }
            pointFlag[k / 100, k % 100] = 1;
            point = k;
        }
    }
    private void Update_Recover()
    {
        myCurrentHealth += myStatus_Recover;
        if (myCurrentHealth > myStatus_MaxHealth) myCurrentHealth = myStatus_MaxHealth;
    }
    private void Update_Move()
    {//移动函数
        if (nextPoint == null) return;
        if (mapHeight == 0) return;
        //        Debug.Log(myCurrentPoint);
        if (nextPoint[myCurrentPoint / 100, myCurrentPoint % 100] == -1)
        {//走到终点，也即基地
            // hide enemy
            this.gameObject.SetActive(false);
            // tell manager lose enemy
            //CS_EnemyManager.Instance.LoseEnemy (this);
            //基地扣血
            CS_GameManager.Instance.LoseLife(reduceHealthPower);
            return;
        }
        Vector3 t_currentPosition = this.transform.position;
        Vector3 t_targetPosition;
        t_targetPosition.x = ((nextPoint[myCurrentPoint / 100, myCurrentPoint % 100] % 100) - CS_GameManager.Instance.myPoint % 100) * 2;
        t_targetPosition.y = this.transform.localPosition.y;
        t_targetPosition.z = (CS_GameManager.Instance.myPoint / 100 - (nextPoint[myCurrentPoint / 100, myCurrentPoint % 100] / 100)) * 2;

        Vector3 t_direction = (t_targetPosition - t_currentPosition).normalized;
        //Debug.Log("thisPosition:" + this.transform.position.x + " " + this.transform.position.z);
        //Debug.Log("speed:" + myCurrentMoveSpeed * Time.fixedDeltaTime);
        Vector3 t_nextPosition = this.transform.position + t_direction * myCurrentMoveSpeed * Time.fixedDeltaTime;
        if ((Mathf.Abs(t_targetPosition.x - this.transform.position.x) < Mathf.Abs(myCurrentMoveSpeed * Time.fixedDeltaTime)
        && Mathf.Abs(t_targetPosition.z - this.transform.position.z) < Mathf.Abs(myCurrentMoveSpeed * Time.fixedDeltaTime))
        || t_nextPosition == t_currentPosition)
        {
            t_nextPosition = t_targetPosition;
            BossSkill();
            myCurrentPoint = nextPoint[myCurrentPoint / 100, myCurrentPoint % 100];
        }
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
        this.transform.position = t_nextPosition;

        // update animation
        // only flip if moving horizontally
        // if (Mathf.Abs (t_direction.x) > Mathf.Abs (t_direction.y)) {
        //     if (t_direction.x > 0) {
        //         mySpriteRenderer.flipX = false;
        //     } else {
        //         mySpriteRenderer.flipX = true;
        //     }
        // }
    }
    public virtual void BossSkill() { }
    public virtual void Update_Attack()
    {//攻击函数
        attackTimer -= Time.fixedDeltaTime;
        if (attackTimer > 0) return;
        attackTimer = myStatus_AttackTime;
        if (targetTower == null) return;
        if (myAnimator != null)
            myAnimator.SetTrigger("Attack");
        targetTower.takeDamage(myStatus_PhysicalAttack, myStatus_MagicAttack);
    }
    public void takeDamage(float physicalDamage, float magicDamage, float realDamage)//受击函数
    {
        if (myShieldNumber > 0 && realDamage == 0)
        {
            myShieldNumber--;
            return;
        }
        physicalDamage -= myStatus_PhysicalDefend;
        if (physicalDamage <= 1) physicalDamage = 1;
        magicDamage *= (1 - myStatus_MagicDefend / 100f);
        Debug.Log(physicalDamage + " " + magicDamage + " " + realDamage);
        myCurrentHealth -= (physicalDamage + magicDamage + realDamage);
        if (myCurrentHealth <= 0)
        {
            CS_GameManager.Instance.LoseEnemy(this);
            myState = State.Dead;
            if (myBirthPrefab != null)
            {
                GameObject t_enemyObject = Instantiate(myBirthPrefab);
                CS_Enemy t_enemy = t_enemyObject.GetComponent<CS_Enemy>();
                t_enemy.Init(this.transform.position, myCurrentPoint);
                CS_GameManager.Instance.addMyEnemyList(t_enemy);
            }
            if (myAnimator != null)
                myAnimator.SetTrigger("Die");
            Dead();
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    public virtual void Dead() { }
    public int getWealth()
    {
        if (wealth == null) return 1;
        return wealth[myCurrentPoint / 100, myCurrentPoint % 100];
    }
    public void loseSpeed(float power)
    {
        loseSpeedTimer += power;
    }
    private void Update_ChangeSpeed()
    {
        loseSpeedTimer -= Time.fixedDeltaTime;
        if (loseSpeedTimer <= 0)
        {
            myCurrentMoveSpeed = myStatus_MoveSpeed;
            loseSpeedTimer = 0;
            return;
        }
        myCurrentMoveSpeed = myStatus_DebuffMoveSpeed;
    }
    public void changeAttack()
    {
        if (!canBarrier) return;
        myState = State.Attack;
    }
    public void changeMove()
    {
        //Debug.Log(this.transform.position.x + " " + this.transform.position.z);
        //Debug.Log("changeMove");
        myState = State.Move;
    }
    public void getTargetTower(CS_GroundTower t_tower)
    {
        targetTower = t_tower;
    }
}
