using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpForce = 4f;
    private Vector2 _movement;
    
    
    //[SerializeField]private Transform _groundCheck;
    //[SerializeField]private float _groundCheckRadius = 0.2f;
    
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
            Debug.Log("Персонаж приземлился → анимация Duck");

            _animator.SetBool("Duck", true);

            // Через 0.2 секунды вызовется метод ResetDuck
            Invoke(nameof(ResetDuck), 0.2f);
        }
    }

    private void ResetDuck()
    {
        _animator.SetBool("Duck", false);
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //_bIsGrounded  = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        _groundCheck.CheckGround();

        _movement.x = Input.GetAxis("Horizontal");
        
        _animator.SetFloat("Move",  Mathf.Abs(_movement.x));
        _animator.SetBool("Jump", false);
        _animator.SetFloat("YVelocity", _rb.velocity.y);
        
        
        if (!_bIsGrounded)
        {
            if (_rb.velocity.y > 0.1f)
            {
                _animator.SetBool("Jump", true);
                _animator.SetBool("Hurt", false);
            }
            else if (_rb.velocity.y < -0.1f)
            {
                _animator.SetBool("Jump", false);
                _animator.SetBool("Hurt", true);
            }
        }
        else
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Hurt", false);
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
        }

    private void OnDisable()
    {
        _groundCheck.OnGroundStateChange -= HandleGroundStateChanged;
    }
}

