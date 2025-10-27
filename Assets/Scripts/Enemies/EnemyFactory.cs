using UnityEngine;
using System.Collections.Generic;
using Enemies;

public enum EnemyType
{
    Slime,
    Fly
}

public class EnemyFactory : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject flyPrefab;

    private Dictionary<EnemyType, GameObject> _enemyPrefabs;
    
    
    public void Init()
    {
        _enemyPrefabs = new Dictionary<EnemyType, GameObject>
        {
            { EnemyType.Slime, slimePrefab },
            { EnemyType.Fly, flyPrefab }
        };
    }

    /*private void Awake()
    {
        _enemyPrefabs = new Dictionary<EnemyType, GameObject>
        {
            { EnemyType.Slime, slimePrefab },
            { EnemyType.Fly, flyPrefab }
        };
    }*/

    public EnemyBase CreateEnemy(EnemyType type, Vector3 position, float speed, int health)
    {
        if (!_enemyPrefabs.TryGetValue(type, out var prefab) || prefab == null)
        {
            Debug.LogError($"❌ Enemy prefab for {type} not assigned!");
            return null;
        }

        var enemyObj = Instantiate(prefab, position, Quaternion.identity);
        var enemy = enemyObj.GetComponent<EnemyBase>();

        if (enemy is IConfigurable configurable)
            configurable.Configure(speed, health);
        else
            Debug.LogWarning($"{type} doesn't implement IConfigurable");

        return enemy;
    }
}