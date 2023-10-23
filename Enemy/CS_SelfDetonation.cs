using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SelfDetonation : CS_Enemy
{
    [SerializeField] float physicalDetonation = 0f;//自爆物伤
    [SerializeField] float magicDetonation = 0f;//自爆法伤
    [SerializeField] float DetonationField = 3f;//自爆范围
    [SerializeField] GameObject myEffectPrefab;
    // Start is called before the first frame update
    public override void Dead()
    {
        Vector3 effectPosition = this.transform.position;
        effectPosition.y += 2.5f;//中心向上移动0.25以保证在cube上面
        GameObject effect = GameObject.Instantiate(myEffectPrefab, effectPosition, Quaternion.identity);
        List<CS_BasicTower> basicTowerList = new List<CS_BasicTower>();
        List<CS_BasicTower> t_basicTowerList = CS_GameManager.Instance.basicTowerList;
        for (int i = 0; i < t_basicTowerList.Count; i++)
        {
            Vector3 t_position = t_basicTowerList[i].transform.position;
            if (
                t_position.x <= this.transform.position.x + DetonationField
                && t_position.x >= this.transform.position.x - DetonationField
                && t_position.z <= this.transform.position.z + DetonationField
                && t_position.z >= this.transform.position.z - DetonationField
            )
            {
                basicTowerList.Add(t_basicTowerList[i]);
            }
        }
        if (basicTowerList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < basicTowerList.Count; i++)
        {
            basicTowerList[i].takeDamage(physicalDetonation, magicDetonation);
        }
        List<CS_GroundTower> groundTowerList = new List<CS_GroundTower>();
        List<CS_GroundTower> t_groundTowerList = CS_GameManager.Instance.groundTowerList;
        for (int i = 0; i < t_groundTowerList.Count; i++)
        {
            Vector3 t_position = t_groundTowerList[i].transform.position;
            if (
                t_position.x <= this.transform.position.x + DetonationField
                && t_position.x >= this.transform.position.x - DetonationField
                && t_position.z <= this.transform.position.z + DetonationField
                && t_position.z >= this.transform.position.z - DetonationField
            )
            {
                groundTowerList.Add(t_groundTowerList[i]);
            }
        }
        if (groundTowerList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < groundTowerList.Count; i++)
        {
            groundTowerList[i].takeDamage(physicalDetonation, magicDetonation);
        }
    }
}
