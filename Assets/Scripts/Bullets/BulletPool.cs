using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _poolSize = 10;

    private List<GameObject> _pool = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.SetActive(false);
            _pool.Add(bullet);
        }
    }

    
    public GameObject GetBullet()
    {
        foreach (var bullet in _pool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        
        return null;
    }
}