using UnityEngine;
using System.Collections.Generic;
using Enemies;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private class EnemySpawnData
    {
        public EnemyType type;
        public Vector2 position;
        public float speed = 1f;
        public int health = 3;
    }
    
    [SerializeField] private EnemyFactory _factory;

    [Header("Spawn Configurations")]
    [SerializeField] private List<EnemySpawnData> _enemiesToSpawn = new List<EnemySpawnData>();
    
    
    private void Start()
    {
        
        foreach (var data in _enemiesToSpawn)
        {
            _factory.CreateEnemy(data.type, data.position, data.speed, data.health);
        }
    }
    
}
