using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDash : InteractionSystem
{
    public Animator anim;
    [SerializeField] AudioData interactedSFX;
    [SerializeField] Text upgradeText;


    // public Upgrade_SO upgrade;
    // bool isTaken;
    // SaveGameManager saveGameManager;

    public void Start()
    {
        if(Player.MyInstance.hasDash == true)
        {
            gameObject.SetActive(false);
        }
    }
    public override void Interact()
    {
        Player.MyInstance.hasDash = true;
        upgradeText.text = "Dash Ability Upgrade";
        UpgradeUI.Instance.CallingMethod();
        AudioSetting.Instance.PlaySFX(interactedSFX);
        anim.SetTrigger("Interacted");
        GameEvents.OnSaveInitiated();
    }


}
