using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum mapCubeType
{
    Variant,
    treeVariant,
    Spawn
}
public class CS_MapCube : MonoBehaviour
{
    [HideInInspector] public GameObject towerGo;//当前cube上的塔（炮台）
    //[HideInInspector] public CS_TowerData towerData;//当前cube上的塔的类型
    [SerializeField] GameObject VariantPrefab;//可修建cube预制体
    [SerializeField] public mapCubeType myCubeType = mapCubeType.Variant;//当前cube类型。
    [SerializeField] GameObject bluePlane;//触碰变色 
    [SerializeField] public bool canBuild;//是否可修建，即已有建筑
    public bool isStone = false;//当前cube上的塔是否是stone
    // Start is called before the first frame update
    void Start()
    {
        if (bluePlane != null)
            bluePlane.SetActive(false);
    }

    public void buildVariant()
    {
        GameObject.Instantiate(VariantPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void buildTower(CS_TowerData towerData)
    {
        if (towerData.type == TowerType.item)
        {
            Vector3 itemPosition = this.transform.position;
            itemPosition.y += 0.25f;//中心向上移动0.25以保证在cube上面
            GameObject.Instantiate(towerData.TowerPrefab, itemPosition, Quaternion.identity);
            return;
        }
        if (!canBuild) return;
        DestroyTower();
        canBuild = false;
        CS_BuildingManager.Instance.selectedTowerData = null;
        //this.towerData = towerData;
        Vector3 position = this.transform.position;
        position.y += 0.25f;//中心向上移动0.25以保证在cube上面
        if (towerData.TowerPrefab == null) return;
        towerGo = GameObject.Instantiate(towerData.TowerPrefab, position, Quaternion.identity);
        CS_BasicTower t_tower = towerGo.GetComponent<CS_BasicTower>();
        if (t_tower != null)
        {
            CS_GameManager.Instance.addMyBasicTower(t_tower);
            t_tower.myMapCube = this;
            t_tower.isAwake = true;
        }
        else
        {
            CS_GroundTower t_groundTower = towerGo.GetComponent<CS_GroundTower>();
            CS_GameManager.Instance.addMyGroundTower(t_groundTower);
            t_groundTower.myMapCube = this;
            t_groundTower.isAwake = true;
        }
        if (bluePlane.activeSelf == true)
        {
            bluePlane.SetActive(false);
        }
    }
    public virtual void DestroyTower()
    {
        if (towerGo == null) return;
        //Debug.Log("destory");
        if (isStone == true)
        {
            isStone = false;
            int thisPoint = CS_GameManager.Instance.myPoint - ((int)this.gameObject.transform.position.z / 2) * 100 - ((int)this.gameObject.transform.position.x / 2);
            thisPoint += CS_BuildingManager.Instance.debugX * 100 + CS_BuildingManager.Instance.debugY;
            CS_GameManager.Instance.maps[thisPoint / 100, thisPoint % 100] = 2;
        }
        Destroy(towerGo);
        canBuild = true;
        //towerData = null;
        //GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1.5f);
    }

    void OnMouseEnter()
    {
        if (bluePlane == null) return;
        if (CS_BuildingManager.Instance.selectedTowerData == null) return;
        if (CS_BuildingManager.Instance.selectedTowerData.TowerPrefab == null) return;
        if (CS_BuildingManager.Instance.selectedTowerData == CS_BuildingManager.Instance.deleteTower)
        {
            if (canBuild == false && towerGo != null)
                bluePlane.SetActive(true);
            return;
        }

        if (CS_BuildingManager.Instance.selectedTowerData.type == TowerType.item)
        {
            bluePlane.SetActive(true);
            return;
        }
        if (towerGo != null) return;
        if (CS_BuildingManager.Instance.selectedTowerData.type == TowerType.GroundTower)
        {
            if (this.myCubeType == mapCubeType.Spawn)
            {
                bluePlane.SetActive(true);
                Vector3 position = this.transform.position;
                position.y += 0.25f;//中心向上移动0.25以保证在cube上面
                if (CS_BuildingManager.Instance.selectedTowerData.TowerPrefab == null) return;
                towerGo = GameObject.Instantiate(CS_BuildingManager.Instance.selectedTowerData.TowerPrefab, position, Quaternion.identity);
            }
            return;
        }
        if (CS_BuildingManager.Instance.selectedTowerData == CS_BuildingManager.Instance.buildCube)
        {
            if (this.myCubeType == mapCubeType.treeVariant)
            {
                bluePlane.SetActive(true);
            }
            return;
        }
        if (CS_BuildingManager.Instance.selectedTowerData.type == TowerType.Stone)
        {
            if (this.myCubeType == mapCubeType.Spawn)
            {
                bluePlane.SetActive(true);
                Vector3 position = this.transform.position;
                position.y += 0.25f;//中心向上移动0.25以保证在cube上面
                if (CS_BuildingManager.Instance.selectedTowerData.TowerPrefab == null) return;
                towerGo = GameObject.Instantiate(CS_BuildingManager.Instance.selectedTowerData.TowerPrefab, position, Quaternion.identity);
            }
            return;
        }
        if (CS_BuildingManager.Instance.selectedTowerData.type == TowerType.BasicTower)
        {
            if (this.myCubeType == mapCubeType.Variant)
            {
                bluePlane.SetActive(true);
                bluePlane.SetActive(true);
                Vector3 position = this.transform.position;
                position.y += 0.25f;//中心向上移动0.25以保证在cube上面
                if (CS_BuildingManager.Instance.selectedTowerData.TowerPrefab == null) return;
                towerGo = GameObject.Instantiate(CS_BuildingManager.Instance.selectedTowerData.TowerPrefab, position, Quaternion.identity);
            }
        }
    }
    void OnMouseExit()
    {
        if (bluePlane == null) return;
        if (bluePlane.activeSelf == true)
        {
            bluePlane.SetActive(false);
        }
        if (towerGo == null) return;
        CS_BasicTower t_tower = towerGo.GetComponent<CS_BasicTower>();
        if (t_tower != null)
        {
            if (t_tower.isAwake != true) DestroyTower();
        }
        else
        {
            CS_GroundTower t_groundTower = towerGo.GetComponent<CS_GroundTower>();
            if (t_groundTower.isAwake != true) DestroyTower();
        }
    }
}
