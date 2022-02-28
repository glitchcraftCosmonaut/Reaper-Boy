using System;
using System.Collections;
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
    public float MyX {get; set;}
    public float MyY {get; set;}
    public float MyZ {get; set;}
    public int currentSceneIndex;


    public PlayerDatas(Vector3 position, PlayerDashData dashData, string sceneName)
    {
        PlayerDashDataName = dashData.name;
        sceneName = SceneManager.GetActiveScene().name;
        MySceneName = sceneName;
        this.MyX = position.x;
        this.MyY = position.y;
        this.MyZ = position.z;
    }
}
