using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        data.MyPlayerData = new PlayerDatas(Player.MyInstance.transform.position, Player.MyInstance.playerDashData, Player.MyInstance.stageName);
    }
    public void LoadPlayer(PlayerSaveData data)
    {
        Player.MyInstance.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY,data.MyPlayerData.MyZ);
        Player.MyInstance.stageName = data.MyPlayerData.MySceneName;
        // SceneManager.LoadScene(data.MyPlayerData.MySceneName);
        Player.MyInstance.playerDashData = Resources.Load<PlayerDashData>(data.MyPlayerData.PlayerDashDataName);
        // Player.MyInstance.playerData = Resources.Load<PlayerData>(data.MyPlayerData.playerDataName);
        Player.MyInstance.hasDash = Player.MyInstance.playerDashData.hasDash;
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
