using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Witch : CS_Enemy
{
    [SerializeField] float Attack = 100000f;//攻击力（特别大）
    public override void Update_Attack()
    {//攻击函数
        if (targetTower != null)
        {
            targetTower.takeDamage(Attack, Attack);
            takeDamage(Attack, Attack, Attack);
        }
    }
}
