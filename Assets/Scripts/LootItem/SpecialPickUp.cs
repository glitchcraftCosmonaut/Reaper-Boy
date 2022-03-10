using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPickUp : LootItem
{
    [SerializeField] int fullHealthScore = 200;
   
    bool isPickUp;
    protected override void Awake()
    {
        base.Awake();
        if (collectibleManager.CollectedItems.Contains(uniqueID.ID))
        {
            gameObject.SetActive(false);
        }
    }

    protected override void PickUp()
    {
        base.PickUp();
        // pickUpSFX = fullHealthPickUpSFX;
        // lootMessage.text = $"SCORE + {fullHealthScore}";
        collectibleManager.AddCoin(fullHealthScore);
        collectibleManager.CollectedItems.Add(uniqueID.ID);

    }
}
