using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] LootSetting[] lootSettings;
    protected Vector2 moveDirection = new Vector2(0,2);


    public void Spawn(Vector2 position)
    {
        foreach (var item in lootSettings)
        {
            item.Spawn(position + moveDirection + Random.insideUnitCircle);
        }
    }
}
