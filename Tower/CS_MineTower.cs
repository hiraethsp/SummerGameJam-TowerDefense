using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTower : CS_BasicTower
{
    [SerializeField] float cost = 100f;//生产量
    [SerializeField] float myStatus_CostTime = 5f;//生产间隔时间
    private float costTimer = 5f;//费用计时器
    public override void Start()
    {
        base.Start();
        costTimer = myStatus_CostTime;
        myAnimator = this.gameObject.GetComponent<Animator>();
    }
    public override void FixedUpdate()
    {
        if (isAwake == false) return;
        if (CS_GameManager.Instance.onPause) return;
        costTimer -= Time.fixedDeltaTime;
        if (costTimer > 0) return;
        CS_GameManager.Instance.getCost(cost);
        myAnimator.SetTrigger("Spell");
        costTimer = myStatus_CostTime;
    }
}
