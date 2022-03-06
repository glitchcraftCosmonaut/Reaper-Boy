public class SaveGameManager : PersistentSingleton<SaveGameManager>
{
    // public static List<Player> player = new List<Player>();
    // // private Player player;
    // [SerializeField] Player playerPrefab;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public PlayerSave playerSave;


    const string PLAYER_KEY = "/player";

    protected override void Awake()
    {
        base.Awake();
        if(playerSave == null)
        {
            playerSave = FindObjectOfType<PlayerSave>();
        }
        
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
        playerSave.SavePlayer(data);
        SaveSystem.Save(data, PLAYER_KEY);
    }

    public void Load()
    {
        if (SaveSystem.SaveExists(PLAYER_KEY))
        {
            data = SaveSystem.Load<PlayerSaveData>(PLAYER_KEY);
            playerSave.LoadPlayer(data);
        }
    }
}
