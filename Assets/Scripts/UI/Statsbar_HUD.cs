using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Statsbar_HUD : Statsbar
{
    [SerializeField] protected Text percentText;
    // [SerializeField] protected FloatValueSO maxHealth;

   
    
    protected virtual void SetPercentText()
    {
        // percentText.text = Mathf.RoundToInt(targetFillAmount * 100f) + "%";
        percentText.text = targetFillAmount.ToString("P0");
    }

    public override void Initialize(float currentValue)
    {
        base.Initialize(currentValue);
        SetPercentText();
    }

    protected override IEnumerator BufferedFillingCoroutine(Image image)
    {
        SetPercentText();
        return base.BufferedFillingCoroutine(image);
    }
}
