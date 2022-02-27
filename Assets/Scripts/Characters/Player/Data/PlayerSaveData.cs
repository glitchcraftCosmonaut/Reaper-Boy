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
    public string playerDashDataName{get; set;}
    // public float[] position {get; set;}
    public float MyX {get; set;}
    public float MyY {get; set;}
    public float MyZ {get; set;}
    public int currentSceneIndex;


    public PlayerDatas(Vector3 position, PlayerDashData dashData)
    {
        playerDashDataName = dashData.name;
        // Vector3 playerPos = player.transform.position;
        this.MyX = position.x;
        this.MyY = position.y;
        this.MyZ = position.z;

        // position = new float[]
        // {
        //     playerPos.x, playerPos.y, playerPos.z
        // };
    }
}
