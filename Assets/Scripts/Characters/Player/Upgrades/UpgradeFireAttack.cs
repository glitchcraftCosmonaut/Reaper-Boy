using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFireAttack : InteractionSystem
{
    public Animator anim;
    [SerializeField] AudioData interactedSFX;
    [SerializeField] Text upgradeText;


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
        upgradeText.text = "Fire Slash Upgrade";
        UpgradeUI.Instance.CallingMethod();
        AudioSetting.Instance.PlaySFX(interactedSFX);
        anim.SetTrigger("Interacted");
        GameEvents.OnSaveInitiated();
    }
}
