using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Statsbar_HUD : Statsbar
{
    [SerializeField] protected Text percentText;

   
    
    protected virtual void SetPercentText()
    {
        percentText.text = Mathf.RoundToInt(targetFillAmount * 100f) + "%";
        // percentText.text = targetFillAmount.ToString("P0");
    }

    // public override void Initialize(float currentValue)
    // {
    //     base.Initialize(currentValue);
    //     SetPercentText();
    // }
    // public override void UpdateStates(float currentValue)
    // {
    //     base.UpdateStates(currentValue);
    //     SetPercentText();
    // }

    protected override IEnumerator BufferedFillingCoroutine(Image image)
    {
        SetPercentText();
        return base.BufferedFillingCoroutine(image);
    }
}
