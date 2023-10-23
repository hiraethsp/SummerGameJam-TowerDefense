using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//总控制，生命值，时间管理
public class CS_GameManager : MonoBehaviour
{
    private static CS_GameManager instance = null;
    public static CS_GameManager Instance { get { return instance; } }
    [SerializeField] public int myMaxLife = 20;//基地生命值上限
    [SerializeField] private int myCurrentLife;//当前生命值
    [SerializeField] private float cost = 0;//钱数
    [SerializeField] float changeCost = 0;//钱数变化
    [SerializeField] float costTime = 5f;//钱几秒增加一次
    private float myCostTimer = 5f;//钱增加计时器
    private float Timer;//计时器
    [SerializeField]
    public int[,] maps;
    public GameObject lose;
    [SerializeField] public int myPoint = 507;//基地位置
    [SerializeField] public int mapWidth = 15;//地图宽度(x轴)
    [SerializeField] public int mapHeight = 11;//地图高度(y轴)
    public List<CS_Enemy> myEnemyList = new List<CS_Enemy>();
    public List<CS_BasicTower> basicTowerList = new List<CS_BasicTower>();
    public List<CS_GroundTower> groundTowerList = new List<CS_GroundTower>();
    public int myKillEnemyCount = 0;//已击败敌人数量
    public bool onPause = false;
    [SerializeField] public GameObject win;
    //[SerializeField]changerTime=
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        onPause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCurrentLife = myMaxLife;
        //当前生命值初始化
        //CS_UIManager.instance.SetLife(myCurrentLife);
        cost = 1000000;//初始cost
        myCostTimer = costTime;
        maps = GetComponent<MapData>().maps;
    }

    // Update is called once per frame
    void Update()
    {
        if (onPause) return;
        myCostTimer -= Time.deltaTime;
        if (myCostTimer > 0) return;
        myCostTimer = costTime;
        cost += changeCost;
    }
    public void LoseLife(int num)
    {
        //扣血
        myCurrentLife -= num;
        //CS_UIManager还没写，后面补上。
        //CS_UIManager.Instance.SetLife (myCurrentLife);
        //生命值没了，gamer over。
        if (myCurrentLife <= 0)
        {
            //CS_UIManager.Instance.ShowPageFail ();
            Time.timeScale = 0;
            lose.SetActive(true);
        }
    }
    public void addMyEnemyList(CS_Enemy t_enemy)
    {
        myEnemyList.Add(t_enemy);
    }
    public void LoseEnemy(CS_Enemy g_enemy)
    {
        // update enemy count
        myKillEnemyCount++;
        //CS_UIManager.Instance.SetCount (myEnemyCount, myEnemySpawnTimeArray.Length);

        // if (myEnemyCount == ) {
        //     CS_UIManager.Instance.ShowPageEnd ();
        // }

        // remove enemy from list
        myEnemyList.Remove(g_enemy);
    }
    public void addMyBasicTower(CS_BasicTower t_basicTower)
    {
        basicTowerList.Add(t_basicTower);
    }
    public void LoseBasicTower(CS_BasicTower t_basicTower)
    {
        basicTowerList.Remove(t_basicTower);
    }
    public void addMyGroundTower(CS_GroundTower t_groundTower)
    {
        groundTowerList.Add(t_groundTower);
    }
    public void LoseGroundTower(CS_GroundTower t_groundTower)
    {
        groundTowerList.Remove(t_groundTower);
    }
    public void getCost(float cost)
    {
        this.cost += cost;
    }
    public float myCost()
    {
        return cost;
    }
    public int getMyHealth()
    {
        return myCurrentLife;
    }
    public void setCost(float cost)
    {
        this.cost = cost;
    }
    public void setMyHealth(int life)
    {
        this.myCurrentLife = life;
    }
    public void myWin()
    {
        Time.timeScale = 0;
        win.SetActive(true);
    }

}
