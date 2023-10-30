using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region PUBLIC_PROPERTIES
    public Rigidbody2D Rigidbody => _rigidBody;
    public float MovementSpeed => _movementSpeed;
    public float JumpForce => _jumpForce;
    #endregion

    #region PRIVATE_PROPERTIES
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    [Header("Jumping Layers")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _platformLayer;
    [Header("Movement Properties")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown = 0.75f;
    private float _currentJumpCooldown;
    private bool _isJumping;
    #endregion

    #region UNITY_FUNCTIONS
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    { 
        _currentJumpCooldown = _jumpCooldown;
    }
    private void Update()
    {
        if (_currentJumpCooldown > 0)
        {
            _currentJumpCooldown -= Time.deltaTime;
        }

        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _groundLayer || collision.gameObject.layer == _platformLayer)
        {
            _isJumping = false;
        }
    }
    #endregion

    #region CUSTOM_FUNCTIONS
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_isJumping && _currentJumpCooldown <= 0)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumping = true;
            _currentJumpCooldown = _jumpCooldown;
        }
    }

    public void Move()
    {
        _rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * MovementSpeed, Rigidbody.velocity.y);
    }
    #endregion
}
