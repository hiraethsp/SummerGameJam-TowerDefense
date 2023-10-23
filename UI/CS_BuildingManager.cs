using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CS_BuildingManager : MonoBehaviour
{
    private static CS_BuildingManager instance = null;
    public static CS_BuildingManager Instance { get { return instance; } }
    //public CS_TowerData basicTowerData;//基础塔
    public CS_TowerData buildCube;//将Cube由不可修建变为可修建
    [SerializeField] public CS_TowerData deleteTower;//拆塔
    [SerializeField] public CS_TowerData stoneTower;//石头
    [SerializeField] public int debugX = 0;
    [SerializeField] public int debugY = 0;
    public CS_TowerData selectedTowerData;//当前选中的塔。(道具也算是塔)
    private CS_MapCube selectedMapCube;//选中的cube
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        selectedTowerData = null;
    }

    // Update is called once per frame
    void Update()
    {
        Update_Select();
    }
    void Update_Select()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        //if (EventSystem.current.IsPointerOverGameObject() != false) return;
        //开发炮台的建造
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("mapCube"));
        if (!isCollider) return;
        CS_MapCube mapCube = hit.collider.GetComponentInParent<CS_MapCube>();
        if (selectedTowerData == null) return;
        if (selectedTowerData == deleteTower && mapCube.canBuild == false && mapCube.towerGo != null)
        {
            mapCube.DestroyTower();
            Shop.Instance.useitem();
            return;
        }
        if (mapCube.towerGo != null)
        {
            CS_BasicTower t_tower = mapCube.towerGo.GetComponent<CS_BasicTower>();
            if (t_tower != null)
            {
                if (t_tower.isAwake == true) return;
            }
            else
            {
                CS_GroundTower t_groundTower = mapCube.towerGo.GetComponent<CS_GroundTower>();
                if (t_groundTower.isAwake == true) return;
            }
        }
        if (selectedTowerData.type == TowerType.GroundTower)
        {
            if (mapCube.myCubeType == mapCubeType.Spawn)
            {
                mapCube.buildTower(selectedTowerData);
                Shop.Instance.useTower();
            }
            return;
        }
        if (selectedTowerData.type == TowerType.Stone)
        {
            if (mapCube.myCubeType == mapCubeType.Spawn)
            {
                List<CS_Enemy> t_enemyList = CS_GameManager.Instance.myEnemyList;
                for (int i = 0; i < t_enemyList.Count; i++)
                {
                    Vector3 t_position = t_enemyList[i].transform.position;
                    if (
                        t_position.x <= mapCube.transform.position.x + 1
                        && t_position.x >= mapCube.transform.position.x - 1
                        && t_position.z <= mapCube.transform.position.z + 1
                        && t_position.z >= mapCube.transform.position.z - 1
                    )
                    {
                        return;
                    }
                }
                int mapHeight = CS_GameManager.Instance.mapHeight;
                int mapWidth = CS_GameManager.Instance.mapWidth;
                int point = CS_GameManager.Instance.myPoint;
                int[,] t_maps = new int[mapHeight, mapWidth];
                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        t_maps[i, j] = CS_GameManager.Instance.maps[i, j];
                    }
                }
                int thisPoint = CS_GameManager.Instance.myPoint - (int)((mapCube.gameObject.transform.position.z / 2) * 100 - (mapCube.gameObject.transform.position.x / 2));
                //thisPoint += debugX * 100 + debugY;
                Debug.Log("thisPoint:" + thisPoint + " " + (int)(mapCube.gameObject.transform.position.z / 2));
                t_maps[thisPoint / 100, thisPoint % 100] = 1;
                int[,] wealth = new int[mapHeight, mapWidth];
                int[,] nextPoint = new int[mapHeight, mapWidth];
                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        wealth[i, j] = 1000;
                    }
                }
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
                while (true)
                {
                    int min = 999;
                    int k = -1;
                    for (int i = 0; i < mapHeight; i++)
                    {
                        for (int j = 0; j < mapWidth; j++)
                        {
                            if (pointFlag[i, j] == 0 && wealth[i, j] < min && (t_maps[i, j] == 2 || t_maps[i, j] == 0) && wealth[i, j] < 99)
                            {
                                k = i * 100 + j;
                                min = wealth[i, j];
                            }
                        }
                    }
                    if (k == -1) break;//说明从当前位置无法到达目标点,也即意味着箱子不能这么放
                    for (int i = 0; i <= 3; i++)
                    {
                        if (k / 100 + pointX[i] >= 0 && k % 100 + pointY[i] >= 0
                        && k / 100 + pointX[i] < mapHeight && k % 100 + pointY[i] < mapWidth)
                        {
                            if (wealth[k / 100 + pointX[i], k % 100 + pointY[i]] > min + 1)
                            {
                                if (t_maps[k / 100 + pointX[i], k % 100 + pointY[i]] == 2)
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
                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        //Debug.Log(t_maps[i, j] + ":  " + i + " " + j + ":" + wealth[i, j]);
                        if (t_maps[i, j] == 2)
                        {
                            if (wealth[i, j] > 100)
                            {
                                // Debug.Log("NO:" + i + " " + j);
                                return;
                            }
                        }
                    }
                }
                //Debug.Log("t_maps:" + t_maps);
                //                Debug.Log("a:" + CS_GameManager.Instance.maps[thisPoint / 100, thisPoint % 100]);
                CS_GameManager.Instance.maps[thisPoint / 100, thisPoint % 100] = 1;
                mapCube.buildTower(selectedTowerData);
                mapCube.isStone = true;
                Shop.Instance.useitem();
            }
            return;
        }
        if (selectedTowerData.type == TowerType.item)
        {
            Shop.Instance.useitem();
            mapCube.buildTower(selectedTowerData);
        }
        if (selectedTowerData == buildCube)
        {
            if (mapCube.myCubeType == mapCubeType.treeVariant)
            {
                mapCube.buildVariant();
            }
            return;
        }
        if (selectedTowerData.type == TowerType.BasicTower)
        {
            if (mapCube.myCubeType == mapCubeType.Variant)
            {
                mapCube.buildTower(selectedTowerData);
                Shop.Instance.useTower();
            }
        }
    }
    public void OnBuildCube()
    {
        selectedTowerData = buildCube;
    }
    public void OnDelete()
    {
        selectedTowerData = deleteTower;
    }
    public void onStone()
    {
        selectedTowerData = stoneTower;
    }
}
