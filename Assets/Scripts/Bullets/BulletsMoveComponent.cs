using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsMoveComponent : MonoBehaviour
{
    [SerializeField] private float _speed;


    void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * _speed)); 
    }
}
