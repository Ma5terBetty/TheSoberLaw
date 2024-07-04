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
    private BaseCharacter _character;
    [Header("Jumping Layers Detection")]
    [SerializeField] private int _groundLayer;
    [SerializeField] private int _platformLayer;
    [SerializeField] private string _platformsTag;
    [Header("Movement Properties")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown = 0.75f;
    private float _movementSpeed;
    private float _currentJumpCooldown;
    private bool _isJumping;

    [SerializeField] private LayerMask _floorLayerMask;
    #endregion

    #region UNITY_FUNCTIONS
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _character = GetComponent<BaseCharacter>();
    }
    private void Start()
    { 
        _currentJumpCooldown = _jumpCooldown;
        _movementSpeed = _character.MovementSpeed;
    }
    private void Update()
    {
        if (GameManager.IsGamePaused || GameManager.Instance.IsGameOver) return;

        if (_currentJumpCooldown > 0)
        {
            _currentJumpCooldown -= Time.deltaTime;
            _isJumping = false;
        }

        Jump();
    }
    private void FixedUpdate()
    {
        if (GameManager.IsGamePaused || GameManager.Instance.IsGameOver) return;
        Move();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_platformsTag) && Input.GetKey(KeyCode.S))
        {
            _boxCollider.isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(_platformsTag))
        {
            _boxCollider.isTrigger = false;
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
        _rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * MovementSpeed * GameManager.Instance.DifficultyLevel.PlayerSpeed, Rigidbody.velocity.y);
    }
    #endregion
}
