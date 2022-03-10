using UnityEngine;

public class EnemySave : MonoBehaviour
{
    public void SaveEnemy(EnemySaveData data)
    {
        // data.MyEnemyData = new EnemyData(Boss_Behaviour.MyInstance.isDeath);
        data.MyEnemyData = new EnemyData(Boss_Behaviour.MyInstance.enemyData, SaveGameManager.Instance.DeathState);
    }
    public void LoadEnemy(EnemySaveData data)
    {
        // Boss_Behaviour.MyInstance.enemyData = Resources.Load<EnemyBehaviourData>(data.MyEnemyData.EnemyDataName);
        // Boss_Behaviour.MyInstance.isDeath = data.MyEnemyData.EnemyDeath;
        SaveGameManager.Instance.DeathState = data.MyEnemyData.DeathState;
    }

}
