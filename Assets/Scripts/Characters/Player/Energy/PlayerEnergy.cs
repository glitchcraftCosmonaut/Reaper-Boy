using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : Singleton<PlayerEnergy>
{
    [SerializeField] public FloatValueSO energy;
    public const float MAX = 100;
    public const int PERCENT = 1;




    protected override void Awake()
    {
        base.Awake();
        // energyBar.Initialize(energy, MAX);
        // Obtain(MAX);
        energy.Value = 1;
    }

    public void Obtain(float value)
    {
        if (energy.Value == MAX) return;
        energy.Value = Mathf.Clamp(energy.Value + value, 0.1f, 1);
        // energyBar.UpdateStates(energy, MAX);
    }

    public void Use(float value)
    {
        energy.Value = Mathf.Clamp(energy.Value - value, 0.1f, 1);
        
    }

    public virtual void RestoreEnergy(float value)
    {
        if(energy.Value == MAX) return;
        // health += value;
        // health = Mathf.Clamp(health, 0f, maxHealth);
        energy.Value = Mathf.Clamp(energy.Value + value, 0.1f, 1);
    }
    public IEnumerator EnergyRegenCoroutine(WaitForSeconds waitTime, float percent)
    {
        while(energy.Value < MAX)
        {
            yield return waitTime;

            RestoreEnergy(energy.Value * percent);
        }
    }

    public bool IsEnough(int value) => energy.Value >= value;
    
}
