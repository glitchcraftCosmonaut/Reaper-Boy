using System;
using System.Collections.Generic;
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
    public bool EnemyDeath {get; set;}
    public HashSet<string> DeathState { get; set; } = new HashSet<string>();



    public EnemyData(EnemyBehaviourData enemyBehaviourData, HashSet<string> deathState)
    {
        EnemyDataName = enemyBehaviourData.name;
        // EnemyDeath = isDeath;
        DeathState = deathState;
    }
}
