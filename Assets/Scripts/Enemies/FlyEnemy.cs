using UnityEngine;
using Enemies;

public class FlyEnemy : EnemyBase, IConfigurable
{
    [SerializeField] private float _flyHeight = 1.5f;
    [SerializeField] private float _flySpeed = 2f;
    [SerializeField] private float _patrolDistance = 3f;

    private Vector3 _startPosition;
    private bool _movingRight = true;
    private float _time;

    private void Start()
    {
        _startPosition = transform.position;
    }

    
    public void Configure(float speed, int health)
    {
        _speed = speed;
        _health = health;
    }

    
    
    public override void Move()
    {
        float direction = _movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * _flySpeed * Time.deltaTime * direction);
        
        _time += Time.deltaTime * _speed;
        float newY = _startPosition.y + Mathf.Sin(_time) * _flyHeight * 0.5f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
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