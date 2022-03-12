using UnityEngine;

public class UpgradeDash : InteractionSystem
{
    public Animator anim;

    // public Upgrade_SO upgrade;
    // bool isTaken;
    // SaveGameManager saveGameManager;

    public void Start()
    {
        // anim = GetComponent<Animator>();
        // isTaken = upgrade.isTaken;
        if(Player.MyInstance.hasDash == true)
        {
            gameObject.SetActive(false);
        }
    }
    public override void Interact()
    {
        // hashSetData.Collectible.Add(uniqueID.ID);
        // isTaken = true;
        // upgrade.isTaken = isTaken;
        Player.MyInstance.hasDash = true;
        anim.SetTrigger("Interacted");
        GameEvents.OnSaveInitiated();
        // playerSave.UpgradeStates.Add(uniqueID.ID);
        // saveGameManager.UpgradeStates.Add(uniqueID.ID);
    }

}
