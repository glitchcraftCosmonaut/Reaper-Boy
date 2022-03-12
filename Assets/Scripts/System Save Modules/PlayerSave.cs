using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public EnemySaveData enemyData {get; set;} = new EnemySaveData();
    private int currentSceneIndex;
    const string PLAYER_KEY = "/player";
    const string PLAYER_COUNT_KEY = "/player.count";
    const string ENEMY_KEY = "Enemy";
    public HashSet<string> UpgradeStates { get; set; } = new HashSet<string>();
    public HashSet<string> DeathState { get; set; } = new HashSet<string>();
    public EnemySave enemySave;
    // public List<Objects> Objects { get; set; } = new List<Objects>();


    




    private void Awake()
    {
        GameEvents.SaveInitiated += Save;
        
        GameEvents.LoadInitiated += Load;
        Load();
    }

    public void SavePlayer(PlayerSaveData data)
    {
        // data.MyPlayerData = new PlayerDatas(Player.MyInstance.transform.position, Player.MyInstance.stageName, 
        // Player.MyInstance.hasDash, Boss_Behaviour.MyInstance.enemyData, Boss_Behaviour.MyInstance.isDeath);
    }
    public void LoadPlayer(PlayerSaveData data)
    {
        Player.MyInstance.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY,data.MyPlayerData.MyZ);
        Player.MyInstance.stageName = data.MyPlayerData.MySceneName;
        Player.MyInstance.hasDash = data.MyPlayerData.MyDash;
        // Player.MyInstance.UpgradeStates = data.MyPlayerData.UpgradeStates;
    }

    

    public void Save()
    {
        SavePlayer(data);
        SaveSystem.Save(data, PLAYER_KEY);
    }

    public void Load()
    {
        if (SaveSystem.SaveExists(PLAYER_KEY))
        {
            data = SaveSystem.Load<PlayerSaveData>(PLAYER_KEY);
            LoadPlayer(data);
        }
    }
}
