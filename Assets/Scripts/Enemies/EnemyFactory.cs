using System;
using UnityEngine;
using System.Collections.Generic;


namespace Enemies
{
    public enum EnemyType
    {
        Slime,
        Fly
    }
    
    public interface IEnemy
    {
        void Init(EnemyFactory factory, EnemyType type, float speed, int health);
    }

    public class EnemyFactory : MonoBehaviour
    {
        [Header("Enemy Prefabs")]
        [SerializeField] private GameObject slimePrefab;
        [SerializeField] private GameObject flyPrefab;

        [Header("Pool Settings")] [SerializeField]
        private int poolSizePerType = 3;

        private Dictionary<EnemyType, GameObject> _enemyPrefabs;
        private Dictionary<EnemyType, Queue<GameObject>> _enemyPools;
        
        public void ReturnToPool(EnemyType type, GameObject enemy)
        {
            var enemyBase = enemy.GetComponent<EnemyBase>();
            enemy.SetActive(false);
            _enemyPools[type].Enqueue(enemy);
        }


        //private void Awake()
       private void FactoryInitialized()
        {
            if (_enemyPrefabs != null && _enemyPools != null)
                return;
            
            _enemyPrefabs = new Dictionary<EnemyType, GameObject>
            {
                { EnemyType.Slime, slimePrefab },
                { EnemyType.Fly, flyPrefab }
            };

            _enemyPools = new Dictionary<EnemyType, Queue<GameObject>>();
            
            foreach (var kvp in _enemyPrefabs)
            {
                var type = kvp.Key;
                var prefab = kvp.Value;

                Queue<GameObject> pool = new Queue<GameObject>();
                for (int i = 0; i < poolSizePerType; i++)
                {
                    GameObject enemy = Instantiate(prefab);
                    enemy.SetActive(false);
                    pool.Enqueue(enemy);
                }

                _enemyPools[type] = pool;
            }
            
        }
        
        
        public EnemyBase CreateEnemy(EnemyType type, Vector3 position, float speed, int health)
        {
            FactoryInitialized();
            
            var prefab = _enemyPrefabs[type];
            
            if (!_enemyPools.TryGetValue(type, out var pool))
            {
                pool = new Queue<GameObject>();
                _enemyPools[type] = pool;
            }
            
            GameObject enemyObj;
            if (pool.Count > 0)
            {
                enemyObj = pool.Dequeue();
            }
            else
            {
                enemyObj = Instantiate(prefab);
            }
            
            enemyObj.transform.position = position;
            enemyObj.SetActive(true);
            EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();
            enemy.Init(this, type, speed, health);
            
            return enemy;
        }
    }
}
