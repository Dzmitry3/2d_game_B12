using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _slimePrefab;
    [SerializeField] private GameObject _flyPrefab;

    public EnemyBase CreateEnemy(string type, Vector3 position)
    {
        GameObject prefab = null;

        switch (type)
        {
            case "Slime": prefab = _slimePrefab; break;
            case "Fly": prefab = _flyPrefab; break;
            default: Debug.LogError("Unknown enemy type: " + type); break;
        }

        if (prefab == null) return null;

        GameObject enemyObj = Instantiate(prefab, position, Quaternion.identity);
        return enemyObj.GetComponent<EnemyBase>();
    }
}