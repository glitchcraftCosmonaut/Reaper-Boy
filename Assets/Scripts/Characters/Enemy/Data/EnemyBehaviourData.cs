using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEnemyData", menuName ="Data/Enemy Data/Behaviour Data")]
public class EnemyBehaviourData : ScriptableObject
{
    // public Transform target;
    public float attackDistance;
    public float attackDamage;
    public float moveSpeed;
    public float timer;
    public bool inRange; // check if player in range
    public bool isDeath;

}
