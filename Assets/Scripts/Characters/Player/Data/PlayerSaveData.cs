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
    public string playerDataName{get; set;}
    public string MySceneName {get; set;}
    public HashSet<string> UpgradeStates { get; set; } = new HashSet<string>();
    public HashSet<string> EnemyDeathState { get; set; } = new HashSet<string>();

    public bool MyDash {get; set;}
    public float MyX {get; set;}
    public float MyY {get; set;}
    public float MyZ {get; set;}
    public int currentSceneIndex;


    public PlayerDatas(Vector3 position,string sceneName, bool dashData, HashSet<string> upgraded, HashSet<string> enemyDeath)
    {
        // PlayerDashDataName = dashData.name;
        this.MyDash = dashData;
        sceneName = SceneManager.GetActiveScene().name;
        MySceneName = sceneName;
        this.MyX = position.x;
        this.MyY = position.y;
        this.MyZ = position.z;
        this.UpgradeStates = upgraded;
        this.EnemyDeathState = enemyDeath;
    }
}
