using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    // public EnemySaveData data {get; set;} = new EnemySaveData();

    // const string ENEMY_KEY = "Enemy";

    //  private void OnEnable()
    // {
    //     // GameEvents.SaveInitiated += Save;
        
    //     // GameEvents.LoadInitiated += Load;
    //     Load();
    // }
    // private void OnDisable()  
    // {
    //     Save();
    // }
    // private void OnApplicationPause(bool pauseStatus)
    // {
    //     Save();
    // }

    public void SaveEnemy(EnemySaveData data)
    {
        data.MyEnemyData = new EnemyData(Boss_Behaviour.MyInstance.enemyData);
    }
    public void LoadEnemy(EnemySaveData data)
    {
        Boss_Behaviour.MyInstance.enemyData = Resources.Load<EnemyBehaviourData>(data.MyEnemyData.EnemyDataName);
        Boss_Behaviour.MyInstance.isDeath = Boss_Behaviour.MyInstance.enemyData.isDeath;
    }

    // public void Save()
    // {
    //     SaveEnemy(data);
    //     SaveSystem.Save(data, ENEMY_KEY);
    // }

    // public void Load()
    // {
    //     if (SaveSystem.SaveExists(ENEMY_KEY))
    //     {
    //         data = SaveSystem.Load<EnemySaveData>(ENEMY_KEY);
    //         LoadEnemy(data);
    //     }
    // }
}
