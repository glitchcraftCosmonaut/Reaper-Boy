using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : PersistentSingleton<CollectibleManager>
{
    public int Coin => coin;
    public int SpecialCollectible => specialCollectible;
    [HideInInspector]public int coin;
    int currentCoin;

    [HideInInspector]public int specialCollectible;
    int currentSpecialCollectible;


    Vector3 coinTextScale = new Vector3(1.2f, 1.2f, 1f);
    Vector3 specialTextScale = new Vector3(1.2f, 1.2f, 1f);

    
    public void ResetScore()
    {
        coin = 0;
        currentCoin = 0;
        CoinDisplay.UpdateText(coin);
        specialCollectible = 0;
        currentSpecialCollectible = 0;
        SpecialCollectDisplay.UpdateText(specialCollectible);
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
}
