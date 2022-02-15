using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newGlobalData", menuName = "Data/FloatData")]
public class FloatValueSO : SingletonScriptableObject<FloatValueSO>
{
    [SerializeField] float _value;

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChange?.Invoke(_value);
        }
    }
    public event Action<float> OnValueChange;
}
