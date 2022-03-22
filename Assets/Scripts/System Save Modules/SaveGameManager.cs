using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : PersistentSingleton<SaveGameManager>
{
    // public static List<Player> player = new List<Player>();
    // // private Player player;
    // [SerializeField] Player playerPrefab;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public EnemySaveData enemyData {get; set;} = new EnemySaveData();
    // public HashSet<string> UpgradeStates { get; set; } = new HashSet<string>();
    public HashSet<string> DeathStates { get; set; } = new HashSet<string>();


    private int currentSceneIndex;
    const string PLAYER_KEY = "/player";

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
            Player.MyInstance.transform.position,Player.MyInstance.stageName, 
            Player.MyInstance.hasDash, Boss_Behaviour.MyInstance.enemyData, Boss_Behaviour.MyInstance.isDeath, Player.MyInstance.hasFireAttack,
            SpecialPickUp.MyInstance.isPickedUp, SpecialPickUp.MyInstance.lootData
            );
    }
    public void LoadPlayer(PlayerSaveData data)
    {
        Player.MyInstance.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY,data.MyPlayerData.MyZ);
        Player.MyInstance.stageName = data.MyPlayerData.MySceneName;
        Player.MyInstance.hasDash = data.MyPlayerData.MyDash;
        Player.MyInstance.hasFireAttack = data.MyPlayerData.MyFireAttack;
        // Player.MyInstance.UpgradeStates = data.MyPlayerData.UpgradeStates;
        Boss_Behaviour.MyInstance.enemyData = Resources.Load<EnemyBehaviourData>(data.MyPlayerData.EnemyDataName);
        Boss_Behaviour.MyInstance.enemyData.isDeath = data.MyPlayerData.EnemyDeath;
        SpecialPickUp.MyInstance.lootData.isPickedUp = data.MyPlayerData.SpecialPickUp;
        SpecialPickUp.MyInstance.isPickedUp = data.MyPlayerData.SpecialPickUp;
        // Boss_Behaviour.MyInstance.isDeath = data.MyPlayerData.EnemyDeathState;

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
