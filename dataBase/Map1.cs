using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MapData
{
    private void Awake()
    {
        maps = new int[6,10]{//数据化地图，高台为1，怪可走为2，终点为0
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,2,2,2,1,1},
        {1,1,1,1,1,2,1,2,1,1},
        {2,2,2,2,2,2,1,2,1,1},
        {1,1,1,1,1,1,1,2,0,1},
        {1,1,1,1,1,1,1,1,1,1}
        };//数据化地图，高台为1，怪可走为2，终点为0
    }
}
