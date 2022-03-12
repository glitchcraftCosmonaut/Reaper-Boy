using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPickUp : LootItem
{
    [SerializeField] int fullHealthScore = 200;
   
    bool isPickUp;

    protected override void PickUp()
    {
        CollectibleManager.Instance.AddSpecialCollectible(fullHealthScore);
        base.PickUp();
        // pickUpSFX = fullHealthPickUpSFX;
        // lootMessage.text = $"SCORE + {fullHealthScore}";
        // collectibleManager.CollectedItems.Add(uniqueID.ID);

    }
}
