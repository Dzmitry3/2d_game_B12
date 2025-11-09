using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private BulletPool _bulletPool;
    
    private MovementComponent _movement;
    private void Awake()
    {
        _movement = GetComponent<MovementComponent>();
    }

    private void OnEnable()
    {
        _movement.OnBulletsSpawn += OnBulletsSpawn;
    }

    
    private void OnBulletsSpawn()
    {
        GameObject bullet = _bulletPool.GetBullet();
        if (bullet == null)
            return; // пул закончился

        // определяем направление
        int direction = _spriteRenderer.flipX ? -1 : 1;
        // активируем пулю
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);

        // передаём направление
        bullet.GetComponent<BulletsMoveComponent>().Init(direction);
        
    }
    private void OnDisable()
    {
        _movement.OnBulletsSpawn -= OnBulletsSpawn;
    }
}