using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : PersistentSingleton<CollectibleManager>
{
    public int Coin => coin;
    public int SpecialCollectible => specialCollectible;
    int coin;
    int currentCoin;

    int specialCollectible;
    int currentSpecialCollectible;


    Vector3 coinTextScale = new Vector3(1.2f, 1.2f, 1f);
    Vector3 specialTextScale = new Vector3(1.2f, 1.2f, 1f);

    protected override void Awake() 
    {
        base.Awake();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
        Load();
    }
    
    public void ResetScore()
    {
        coin = 0;
        currentCoin = 0;
        CoinDisplay.UpdateText(coin);
    }

    public void AddCoin(int coinPoint)
    {
        currentCoin += coinPoint;
        StartCoroutine(nameof(AddCoinCoroutine));
    }

    IEnumerator AddCoinCoroutine()
    {
        CoinDisplay.ScaleText(coinTextScale);
        while(coin < currentCoin)
        {
            coin += 1;
            CoinDisplay.UpdateText(coin);

            yield return null;
        }
        CoinDisplay.ScaleText(Vector3.one);
    }

    public void AddSpecialCollectible(int specialPoint)
    {
        currentSpecialCollectible += specialPoint;
        StartCoroutine(nameof(AddSpecialCoroutine));
    }

    IEnumerator AddSpecialCoroutine()
    {
        SpecialCollectDisplay.ScaleText(specialTextScale);
        while(specialCollectible < currentSpecialCollectible)
        {
            specialCollectible += 1;
            SpecialCollectDisplay.UpdateText(specialCollectible);

            yield return null;
        }
        SpecialCollectDisplay.ScaleText(Vector3.one);
    }

    [Serializable]
    public class PlayerCollectData
    {
        public PlayerCollectible MyCollectible {get; set;}
    }

    [System.Serializable]
    public class PlayerCollectible
    {
        public int coin {get; set;}
        public int specialCollectible {get; set;}
        public HashSet<string> CollectedItems { get; set; } = new HashSet<string>();

        // public string playerName;

        public PlayerCollectible(int coin, int specialCollectible)
        {
            this.coin = coin;
            this.specialCollectible = specialCollectible;
            // this.playerName = playerName;
        }
    }

    readonly string SaveFileName = "playerscore";
    public PlayerCollectData data { get; set; } = new PlayerCollectData();

    public void SavePlayerScoreData(PlayerCollectData data)
    {
        data.MyCollectible = new PlayerCollectible(coin, specialCollectible);
    }

    public void LoadPlayerCollectibleData(PlayerCollectData data)
    {
        coin = data.MyCollectible.coin;
        specialCollectible = data.MyCollectible.specialCollectible;
    }

    public void Save()
    {
        SavePlayerScoreData(data);
        SaveSystem.Save(data, SaveFileName);
    }
    public void Load()
    {
        if(SaveSystem.SaveExists(SaveFileName))
        {
            data = SaveSystem.Load<PlayerCollectData>(SaveFileName);
            LoadPlayerCollectibleData(data);
            // SaveSystem.Save(data,SaveFileName);
        }
    }
}
