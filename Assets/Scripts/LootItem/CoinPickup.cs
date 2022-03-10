using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : LootItem
{
    [SerializeField] int fullHealthScore = 200;
    [SerializeField] float shieldBonus = 20f;
    protected override void PickUp()
    {
        // pickUpSFX = fullHealthPickUpSFX;
        // lootMessage.text = $"SCORE + {fullHealthScore}";
        collectibleManager.AddCoin(fullHealthScore);
        base.PickUp();
    }
}
