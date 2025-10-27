using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public EnemyType type;
    public Vector3 position;
    public float speed = 1f;
    public int health = 3;
}