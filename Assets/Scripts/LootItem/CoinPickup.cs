using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : LootItem
{
    [SerializeField] int coingPoint = 1;
    [SerializeField] AudioData coinPickUp;
    protected override void PickUp()
    {
        // pickUpSFX = fullHealthPickUpSFX;
        // lootMessage.text = $"SCORE + {fullHealthScore}";
        base.PickUp();
        AudioSetting.Instance.PlaySFX(coinPickUp);
        CollectibleManager.Instance.AddCoin(coingPoint);
    }
}
