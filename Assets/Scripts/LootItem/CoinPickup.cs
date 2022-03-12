using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : LootItem
{
    [SerializeField] int coingPoint = 1;
    protected override void PickUp()
    {
        // pickUpSFX = fullHealthPickUpSFX;
        // lootMessage.text = $"SCORE + {fullHealthScore}";
        base.PickUp();
        CollectibleManager.Instance.AddCoin(coingPoint);
    }
}
