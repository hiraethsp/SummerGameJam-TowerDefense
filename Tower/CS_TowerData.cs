
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CS_TowerData
{
    public GameObject TowerPrefab;
    public TowerType type;
}
public enum TowerType
{
    BasicTower,
    GroundTower,
    item,
    Stone
}