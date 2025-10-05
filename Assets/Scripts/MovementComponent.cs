using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpForce = 4f;
    private Vector2 _movement;
    
    [SerializeField]private Transform _groundCheck;
    [SerializeField]private float _groundCheckRadius = 0.2f;
    [SerializeField]private LayerMask _groundLayer;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private bool _bIsGrounded;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        _bIsGrounded  = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        
        _movement.x = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_movement.x * _speed, _rb.velocity.y);
        
        if(_movement.x > 0)
            _spriteRenderer.flipX = false;
        else if(_movement.x < 0)
            _spriteRenderer.flipX = true;
        
        if (Input.GetButtonDown("Jump") && _bIsGrounded)
            {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }

    }

}
