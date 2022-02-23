using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour, IDamageable
{

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");
        Destroy(gameObject);
    }
}
