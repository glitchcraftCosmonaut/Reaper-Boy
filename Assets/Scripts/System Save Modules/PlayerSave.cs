using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    private int currentSceneIndex;
    const string PLAYER_KEY = "/player";
    const string PLAYER_COUNT_KEY = "/player.count";


    private void Awake()
    {
        // GameEvents.SaveInitiated += Save;
        
        // GameEvents.LoadInitiated += Load;
        Load();
    }
    private void OnApplicationQuit() 
    {
        Save();
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        Save();
    }

    public void SavePlayer(PlayerSaveData data)
    {
        data.MyPlayerData = new PlayerDatas(Player.MyInstance.transform.position, Player.MyInstance.playerDashData);
    }
    public void LoadPlayer(PlayerSaveData data)
    {
        Player.MyInstance.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY,data.MyPlayerData.MyZ);
        Player.MyInstance.playerDashData = Resources.Load<PlayerDashData>(data.MyPlayerData.playerDashDataName);
        Player.MyInstance.hasDash = Player.MyInstance.playerDashData;
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
            // currentSceneIndex = SaveLoad.Load<int>("SavedScene");
            LoadPlayer(data);
        }
    }
}
