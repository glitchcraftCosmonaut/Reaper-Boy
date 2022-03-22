using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPickUp : LootItem
{
    [SerializeField] public LootData lootData;
    // [SerializeField] int specialPoint = 1;
    [SerializeField] AudioData specialSFX;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public bool isPickedUp;
    const string PLAYER_KEY = "/player";
    public static SpecialPickUp instance;
    public static SpecialPickUp MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SpecialPickUp>();
            }
            return instance;
        }
    }


    protected override void Awake()
    {
        base.Awake();
        if(SaveSystem.SaveExists(PLAYER_KEY))
        {
            data = SaveSystem.Load<PlayerSaveData>(PLAYER_KEY);
            Debug.Log(data.MyPlayerData.SpecialPickUp);
            if(data.MyPlayerData.SpecialPickUp == true)
            {
                isPickedUp = lootData.isPickedUp;
                gameObject.SetActive(false);
            }
            if(data.MyPlayerData.SpecialPickUp == false)
            {
                lootData.isPickedUp = isPickedUp;
                gameObject.SetActive(true);
            }
        }
        if(!SaveSystem.SaveExists(PLAYER_KEY))
        {
            lootData.isPickedUp = isPickedUp;
            gameObject.SetActive(true);
        }
    }

    protected override void PickUp()
    {
        base.PickUp();
        isPickedUp = true;
        lootData.isPickedUp = isPickedUp;
        // GameEvents.OnSaveInitiated();
        CollectibleManager.Instance.AddSpecialCollectible(lootData.pickUpPoint);
        AudioSetting.Instance.PlaySFX(specialSFX);
    }
}
