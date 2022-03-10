using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public int Coin => coin;
    int coin;
    int currentCoin;
    public HashSet<string> CollectedItems { get; set; } = new HashSet<string>();


    Vector3 coinTextScale = new Vector3(1.2f, 1.2f, 1f);

    private void Awake() 
    {
        // base.Awake();
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

    [Serializable]
    public class PlayerCollectData
    {
        public PlayerCollectible MyCollectible {get; set;}
    }

    [System.Serializable]
    public class PlayerCollectible
    {
        public int coin {get; set;}
        public HashSet<string> CollectedItems { get; set; } = new HashSet<string>();

        // public string playerName;

        public PlayerCollectible(int coin, HashSet<string> collected)
        {
            this.coin = coin;
            this.CollectedItems = collected;
            // this.playerName = playerName;
        }
    }

    readonly string SaveFileName = "playerscore";
    public PlayerCollectData data { get; set; } = new PlayerCollectData();

    public void SavePlayerScoreData(PlayerCollectData data)
    {
        data.MyCollectible = new PlayerCollectible(coin, CollectedItems);
    }

    public void LoadPlayerCollectibleData(PlayerCollectData data)
    {
        coin = data.MyCollectible.coin;
        CollectedItems = data.MyCollectible.CollectedItems;
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
