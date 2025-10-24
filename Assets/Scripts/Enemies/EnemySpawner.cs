using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory _factory;

    private void Start()
    {
        _factory.CreateEnemy("Slime", new Vector3(3, 0, 0));
        _factory.CreateEnemy("Fly", new Vector3(-4, 2, 0));
    }
}