using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Energy Data")]
public class PlayerEnergy : SingletonScriptableObject<PlayerEnergy>
{
    [SerializeField] Energybar energyBar;

    bool available = true;

    public const int MAX = 100;
    public const int PERCENT = 1;
    int energy;



    void Start()
    {
        // energyBar.Initialize(energy, MAX);
        // Obtain(MAX);
    }

    public void Obtain(int value)
    {
        if (energy == MAX || !available) return;

        energy = Mathf.Clamp(energy + value, 0, MAX);
        // energyBar.UpdateStates(energy, MAX);
    }

    public void Use(int value)
    {
        // energy -= value;
        energy = Mathf.Clamp(energy - value, 0, MAX);

        // energyBar.UpdateStates(energy, MAX);

        // if player is overdriving and energy = 0
        if (energy == 0 && !available)
        {
        }
    }

    public bool IsEnough(int value) => energy >= value;
    
}
