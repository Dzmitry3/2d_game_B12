using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    /*private GameObject[] _childObjects;
    private Transform _bulletSpawnPoint;*/
    
    private MovementComponent _movement;
    private void Awake()
    {
        _movement = GetComponent<MovementComponent>();
        //_childObjects = GetComponentsInChildren<GameObject>();
    }

    private void OnEnable()
    {
        _movement.OnBulletsSpawn += OnBulletsSpawn;
    }

    private void OnBulletsSpawn()
    {
        /*foreach (var go in _childObjects)
        {
            if (go.CompareTag("BulletsSpawnPoint "))
            {
                _bulletSpawnPoint = go.transform;
            }
        }*/
        
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        
    }
    private void OnDisable()
    {
        _movement.OnBulletsSpawn -= OnBulletsSpawn;
    }
}