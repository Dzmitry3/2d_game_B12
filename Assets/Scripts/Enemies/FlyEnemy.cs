using UnityEngine;
using Enemies;

public class FlyEnemy : EnemyBase, IEnemy
{
    private bool _movingRight = true;
    private Vector3 _startPosition;

    [SerializeField] private float _patrolDistance = 3f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    
    public override void Init(EnemyFactory factory, EnemyType type, float speed, int health)
    {
        base.Init(factory, type, speed, health);
    }

    
    
    public override void Move()
    {
        float direction = _movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * _speed * Time.deltaTime * direction);
        
        float distanceFromStart = transform.position.x - _startPosition.x;
        if (Mathf.Abs(distanceFromStart) >= _patrolDistance)
        {
            _movingRight = !_movingRight;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Update()
    {
        Move();
    }
}