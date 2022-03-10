using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : PersistentSingleton<SaveGameManager>
{
    // public static List<Player> player = new List<Player>();
    // // private Player player;
    // [SerializeField] Player playerPrefab;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public EnemySaveData enemyData {get; set;} = new EnemySaveData();
    public HashSet<string> UpgradeStates { get; set; } = new HashSet<string>();
    public HashSet<string> DeathState { get; set; } = new HashSet<string>();
    private int currentSceneIndex;
    const string PLAYER_KEY = "/player";
    const string PLAYER_COUNT_KEY = "/player.count";
    const string ENEMY_KEY = "Enemy";
    public EnemySave enemySave;

    // protected override void Awake()
    // {
        
    //     GameEvents.SaveInitiated += Save;
        
    //     GameEvents.LoadInitiated += Load;
    //     Load();
    // }

    
    // public void Save()
    // {
    //     enemySave.SaveEnemy(data);
    //     SaveSystem.Save(data, ENEMY_KEY);
    // }

    // public void Load()
    // {
    //     if (SaveSystem.SaveExists(ENEMY_KEY))
    //     {
    //         data = SaveSystem.Load<EnemySaveData>(ENEMY_KEY);
    //         enemySave.LoadEnemy(data);
    //     }
    // }
    protected override void Awake()
    {
        base.Awake();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
        Load();
    }

    public void SavePlayer(PlayerSaveData data)
    {
        data.MyPlayerData = new PlayerDatas(
            Player.MyInstance.transform.position,Player.MyInstance.stageName, Player.MyInstance.hasDash, UpgradeStates, DeathState
            );
    }
    public void LoadPlayer(PlayerSaveData data)
    {
        Player.MyInstance.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY,data.MyPlayerData.MyZ);
        Player.MyInstance.stageName = data.MyPlayerData.MySceneName;
        Player.MyInstance.hasDash = data.MyPlayerData.MyDash;
        UpgradeStates = data.MyPlayerData.UpgradeStates;
        DeathState = data.MyPlayerData.EnemyDeathState;
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
