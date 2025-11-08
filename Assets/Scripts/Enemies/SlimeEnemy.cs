using UnityEngine;
using Enemies;

public class SlimeEnemy : EnemyBase, IEnemy
{
    private bool _movingRight = true;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkDistance = 0.5f;

    
    
    public override void Init(EnemyFactory factory, EnemyType type, float speed, int health)
    {
        base.Init(factory, type, speed, health);
    }
    
    
    public override void Move()
    {
        float direction = _movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * _speed * Time.deltaTime * direction);
        
        if (!Physics2D.Raycast(_groundCheck.position, Vector2.down, _checkDistance, _groundLayer))
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
