using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    // public static List<Player> player = new List<Player>();
    // // private Player player;
    // [SerializeField] Player playerPrefab;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public PlayerSave playerSave;


    const string PLAYER_KEY = "/player";

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        
        GameEvents.LoadInitiated += Load;
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

    public void Save()
    {
        // string key = PLAYER_KEY + SceneManager.GetActiveScene().buildIndex;
        // // string key = PLAYER_KEY;
        // string countKey = PLAYER_COUNT_KEY + SceneManager.GetActiveScene().buildIndex;
        // SaveSystem.Save(player, countKey);
        // PlayerSaveData data = new PlayerSaveData(Player.MyInstance);
        // SaveSystem.Save(data, key);
        // for(int i = 0; i < player.Count; i++)
        // {
        //     PlayerSaveData data = new PlayerSaveData(player[i]);
        //     SaveSystem.Save(data, key + i);
        // }
        // PlayerSaveData data = new PlayerSaveData();
        playerSave.SavePlayer(data);
        // currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SaveLoad.Save(currentSceneIndex,"SavedScene");
        SaveSystem.Save(data, PLAYER_KEY);
    }

    public void Load()
    {
        // string key = PLAYER_KEY + SceneManager.GetActiveScene().buildIndex; 
        // string countKey = PLAYER_COUNT_KEY + SceneManager.GetActiveScene().buildIndex;

        // int count = SaveSystem.Load<int>(countKey);
        // Player player = Instantiate(playerPrefab);
        // PlayerSaveData data = SaveSystem.Load<PlayerSaveData>(key);
        // PlayerSave.Load(data);

        // for(int i = 0; i < count; i++)
        // {
        //     Player player = Instantiate(playerPrefab);
        //     PlayerSaveData data = SaveSystem.Load<PlayerSaveData>(key + i);
        //     player.Load(data);
        //     player.Initialize();
        // }
        if (SaveSystem.SaveExists(PLAYER_KEY))
        {
            data = SaveSystem.Load<PlayerSaveData>(PLAYER_KEY);
            // currentSceneIndex = SaveLoad.Load<int>("SavedScene");
            playerSave.LoadPlayer(data);
        }
    }
}
