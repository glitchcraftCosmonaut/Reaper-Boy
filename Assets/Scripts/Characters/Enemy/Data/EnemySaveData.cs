using System;
using UnityEngine;

[Serializable]
public class EnemySaveData
{
    public EnemyData MyEnemyData {get; set;}
}

[System.Serializable]

public class EnemyData
{
    public string EnemyDataName{get; set;}


    public EnemyData(EnemyBehaviourData enemyBehaviourData)
    {
        EnemyDataName = enemyBehaviourData.name;
    }
}
