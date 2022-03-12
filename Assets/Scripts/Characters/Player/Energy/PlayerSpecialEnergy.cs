using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialEnergy : Singleton<PlayerSpecialEnergy>
{
    [SerializeField] public FloatValueSO specialEnergy;
    public const float MAX = 100;
    public const int PERCENT = 1;


    protected override void Awake()
    {
        base.Awake();
        // energyBar.Initialize(energy, MAX);
        // Obtain(MAX);
        specialEnergy.Value = 1;
    }

    public void Obtain(float value)
    {
        if (specialEnergy.Value == MAX) return;
       specialEnergy.Value = Mathf.Clamp(specialEnergy.Value + value, 0, 1);
        // energyBar.UpdateStates(energy, MAX);
    }

    public void Use(float value)
    {
        if(specialEnergy.Value == 0) return;
        // energy -= value;
        // energy.Value = Mathf.Clamp(value/MAX, 0f, MAX);
        
        // energy.Value = Mathf.Clamp(energy.Value - value, 0, MAX);
        specialEnergy.Value = Mathf.Clamp(specialEnergy.Value - value, 0, 1);
        // energy.Value -= value /MAX;
        
    }

    public virtual void RestoreEnergy(float value)
    {
        if(specialEnergy.Value == MAX) return;
        // health += value;
        // health = Mathf.Clamp(health, 0f, maxHealth);
        specialEnergy.Value = Mathf.Clamp(specialEnergy.Value + value, 0f, 1);
    }
    public IEnumerator SpecialRegenerationCoroutine(WaitForSeconds waitTime, float percent)
    {
        while(specialEnergy.Value < MAX)
        {
            yield return waitTime;

            RestoreEnergy(specialEnergy.Value * percent);
        }
    }

    public bool IsEnough(int value) => specialEnergy.Value >= value;
}
