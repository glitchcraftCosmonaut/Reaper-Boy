public class SaveGameManager : PersistentSingleton<SaveGameManager>
{
    // public static List<Player> player = new List<Player>();
    // // private Player player;
    // [SerializeField] Player playerPrefab;
     public EnemySaveData data {get; set;} = new EnemySaveData();

    const string ENEMY_KEY = "Enemy";
    public EnemySave enemySave;

    protected override void Awake()
    {
        // base.Awake();
        // if(playerSave == null)
        // {
        //     playerSave = FindObjectOfType<PlayerSave>();
        // }
        
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
        enemySave.SaveEnemy(data);
        SaveSystem.Save(data, ENEMY_KEY);
    }

    public void Load()
    {
        if (SaveSystem.SaveExists(ENEMY_KEY))
        {
            data = SaveSystem.Load<EnemySaveData>(ENEMY_KEY);
            enemySave.LoadEnemy(data);
        }
    }
}
