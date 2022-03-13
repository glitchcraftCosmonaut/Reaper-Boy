using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerSaveData
{
    public PlayerDatas MyPlayerData {get; set;}  
}

[System.Serializable]

public class PlayerDatas
{
    public string PlayerDashDataName{get; set;}
    public string EnemyDataName{get; set;}
    public string LootDataName {get; set;}
    public string playerDataName{get; set;}
    public string MySceneName {get; set;}
    // public HashSet<string> UpgradeStates { get; set; } = new HashSet<string>();
    public HashSet<string> EnemyDeathState { get; set; } = new HashSet<string>();
    // public List<Objects> Objects {get; set;} = new List<Objects>();

    public bool MyDash {get; set;}
    public bool MyFireAttack {get; set;}
    public float MyX {get; set;}
    public float MyY {get; set;}
    public float MyZ {get; set;}
    public bool EnemyDeath {get; set;}
    public bool SpecialPickUp {get; set;}
    public int currentSceneIndex;


    public PlayerDatas(Vector3 position,string sceneName, bool dashData, 
    EnemyBehaviourData enemyBehaviourData, bool enemyDeath, bool fireAttack, bool pickedUp, LootData lootData)
    {
        // PlayerDashDataName = dashData.name;
        this.MyDash = dashData;
        this.MyFireAttack = fireAttack;
        sceneName = SceneManager.GetActiveScene().name;
        MySceneName = sceneName;
        this.MyX = position.x;
        this.MyY = position.y;
        this.MyZ = position.z;
        this.EnemyDataName = enemyBehaviourData.name;
        this.EnemyDeath = enemyDeath;
        this.SpecialPickUp = pickedUp;
        this.LootDataName = lootData.name;

        // this.EnemyDeathState = enemyDeath;
    }
}
