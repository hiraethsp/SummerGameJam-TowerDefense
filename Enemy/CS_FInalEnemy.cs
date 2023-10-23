using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_FInalEnemy : CS_Enemy
{
    public override void Dead()
    {
        CS_GameManager.Instance.myWin();
    }
}
