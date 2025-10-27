using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory _factory;

    [Header("Spawn Configurations")]
    [SerializeField] private List<EnemySpawnData> _enemiesToSpawn = new List<EnemySpawnData>();

    private void Start()
    {
        _factory.Init();
        foreach (var data in _enemiesToSpawn)
        {
            _factory.CreateEnemy(data.type, data.position, data.speed, data.health);
        }
    }
}