using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

public class SlimeEnemy : EnemyBase, IConfigurable
{
    private bool _movingRight = true;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkDistance = 0.5f;

    
    
    public void Configure(float speed, int health)
    {
        _speed = speed;
        _health = health;
    }
    
    
    public override void Move()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime * (_movingRight ? 1 : -1));

        RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, _checkDistance, _groundLayer);
        if (!hit)
            _movingRight = !_movingRight;

        transform.localScale = new Vector3(_movingRight ? -1 : 1, 1, 1);
    }

    private void Update()
    {
        Move();
    }
}
