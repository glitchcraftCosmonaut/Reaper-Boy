using UnityEngine;

public class EnemySave : MonoBehaviour
{
    public void SaveEnemy(EnemySaveData data)
    {
        data.MyEnemyData = new EnemyData(Boss_Behaviour.MyInstance.enemyData);
        // data.MyEnemyData = new EnemyData(Boss_Behaviour.MyInstance.enemyData, Boss_Behaviour.MyInstance.DeathState);
    }
    public void LoadEnemy(EnemySaveData data)
    {
        Boss_Behaviour.MyInstance.enemyData = Resources.Load<EnemyBehaviourData>(data.MyEnemyData.EnemyDataName);
        Boss_Behaviour.MyInstance.isDeath = data.MyEnemyData.EnemyDeath;
        // Boss_Behaviour.MyInstance.DeathState = data.MyEnemyData.DeathState;
    }

}
