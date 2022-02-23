using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyAttackStateData", menuName = "Data/Enemy Data/Attack State")]
public class EnemyAttackData : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 10f;

    public LayerMask whatIsPlayer;
}
