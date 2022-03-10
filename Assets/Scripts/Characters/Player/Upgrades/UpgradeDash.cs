using UnityEngine;

public class UpgradeDash : InteractionSystem
{

    Animator anim;
    Collider2D spriteCollider;
    PlayerSave playerSave;
    SaveGameManager saveGameManager;
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    UniqueID uniqueID;

    public override void Start()
    {
        anim = GetComponent<Animator>();
        spriteCollider = GetComponent<Collider2D>();
        playerSave = FindObjectOfType<PlayerSave>();
        saveGameManager = FindObjectOfType<SaveGameManager>();
        uniqueID = GetComponent<UniqueID>();
        
        // Debug.Log(playerSave.UpgradeStates.Contains(uniqueID.ID));
        Debug.Log(Player.MyInstance.UpgradeStates.Contains(uniqueID.ID));
        if(playerSave.UpgradeStates.Contains(uniqueID.ID))
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public override void Interact()
    {
        Player.MyInstance.UpgradeStates.Add(uniqueID.ID);
        Debug.Log(Player.MyInstance.UpgradeStates.Add(uniqueID.ID));
        Player.MyInstance.hasDash = true;
        anim.SetTrigger("Interacted");
        spriteCollider.enabled = false;
        // playerSave.UpgradeStates.Add(uniqueID.ID);
        // saveGameManager.UpgradeStates.Add(uniqueID.ID);

    }

}
