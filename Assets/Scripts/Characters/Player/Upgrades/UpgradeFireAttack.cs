using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFireAttack : InteractionSystem
{
    public Animator anim;

    // public Upgrade_SO upgrade;
    // bool isTaken;
    // SaveGameManager saveGameManager;

    public void Start()
    {
        // anim = GetComponent<Animator>();
        // isTaken = upgrade.isTaken;
        if(Player.MyInstance.hasFireAttack == true)
        {
            gameObject.SetActive(false);
        }
    }
    public override void Interact()
    {
        Player.MyInstance.hasFireAttack = true;
        anim.SetTrigger("Interacted");
        GameEvents.OnSaveInitiated();
    }
}
