using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newLootData", menuName ="Data/Loot Data/Base Data")]
public class LootData : ScriptableObject
{
    public int pickUpPoint;
    public bool isPickedUp;
}
