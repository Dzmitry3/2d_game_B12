using System;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public Action OnBulletsSpawn;
    
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpForce = 4f;
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GroundChecker _groundCheck;
    private bool _bIsGrounded;
    private int[] _numbers;
    private bool _facingRight = true;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _groundCheck = GetComponentInChildren<GroundChecker>();
    }


    private void OnEnable()
    {
        _groundCheck.OnGroundStateChange += HandleGroundStateChanged;
    }

    private void HandleGroundStateChanged(bool grounded)
    {
        _bIsGrounded = grounded;
        
        if (grounded)
        {
            _animator.SetBool("Fall", false);
            _animator.SetBool("Landing", true);
            Invoke(nameof(ResetLanding), 0.3f);
        }
        else
        {
            _animator.SetBool("Jump", true);
            _animator.SetBool("Fall", false);
        }
    }

    private void ResetLanding()
    {
        _animator.SetBool("Landing", false);
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _groundCheck.CheckGround();

        _movement.x = Input.GetAxis("Horizontal");
        
        _animator.SetFloat("Move",  Mathf.Abs(_movement.x));
        
       
        if (!_bIsGrounded)
            if (_rb.velocity.y > 0.1f)
            {
                _animator.SetBool("Jump", true);
                _animator.SetBool("Fall", false);
            }
            else if (_rb.velocity.y < -0.1f)
            {
                _animator.SetBool("Jump", false);
                _animator.SetBool("Fall", true);
            }
        else
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", false);
        }

        
        _rb.velocity = new Vector2(_movement.x * _speed, _rb.velocity.y);

        if (_movement.x > 0 && !_facingRight)
        {
            _facingRight = true;
            _spriteRenderer.flipX = false;
        }
        else if (_movement.x < 0 && _facingRight)
        {
            _facingRight = false;
            _spriteRenderer.flipX = true;
        }

        if (Input.GetButtonDown("Jump") && _bIsGrounded)
            {
                _animator.SetBool("Jump", true);
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }


        if (Input.GetMouseButtonDown(0))
        {
            OnBulletsSpawn?.Invoke();
        }


    }

    private void OnDisable()
    {
        _groundCheck.OnGroundStateChange -= HandleGroundStateChanged;
    }
}

