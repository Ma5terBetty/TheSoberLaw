using System;
using UnityEngine;

public class Enemy : BaseCharacter, IDamageable, IShooter
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private BaseWeapon _weapon;
    private HealthController _healthController = new HealthController();
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _SightDistance = 3f;
    [SerializeField] private float _patrolDistance;
    [SerializeField] private bool _isPatrolling;
    [SerializeField] private LayerMask _bordersLayerMask;
    [SerializeField] private LayerMask _scenarioLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;
    private bool _isAttacking;
    private bool _isGrounded;
    private bool _isMoving;
    private bool _isLookingForward;
    private Vector3 _initialPosition;
    #endregion

    #region PUBLIC_PROPERTIES
    public HealthController HealthController => _healthController;
    public BaseWeapon BaseWeapon => _weapon;
    public bool IsPlayer() => _isPlayer;
    public bool IsAttacking => _isAttacking;
    public bool IsGrounded => _isGrounded;
    public bool IsMoving => _isMoving;
    #endregion

    #region UNITY_FUNCTIONS
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isPlayer = GetComponent<Player>() == null ? false : true;
    }
    void Start()
    {
        _healthController.Initialize(MaxLife);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        _isMoving = true;
        _isLookingForward = true;
        _isGrounded = false;
        _initialPosition = transform.position;
        if (_patrolDistance > 0) _isPatrolling = true;
    }
    void Update()
    {
        if (GameManager.IsGamePaused) return;
        if (_healthController.CurrentLife <= 0)
        {
            EventManager.Instance.EnemyKilled();
            Destroy(this.gameObject);
        }

        if (IsPlayerInSight())
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.IsGamePaused) return;

        if (_isMoving)
        {
            Move();
        }
    }
    private void LateUpdate()
    {
        CheckBorders();

        if (_isPatrolling)
        {
            if (Vector3.Distance(transform.position, _initialPosition) > _patrolDistance)
            {
                Turn();
                Vector3 aux = (transform.position - _initialPosition).normalized;
                transform.position = transform.position - aux;
            }
        }
    }
    #endregion

    #region COLLISION_AND_TRIGGERS
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == _scenarioLayerMask) _isGrounded = true;
        else _isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.gameObject.GetComponent<PrefabBullet>();
        if (bullet != null)
        {
            if (bullet.IsFromPlayer)
            {
                GetDamage(bullet.DamageAmount);
                print($"{HealthController.CurrentLife}");
                other.gameObject.GetComponent<PrefabBullet>().DestroyBullet();

                if (!_isAttacking)
                {
                    Turn();
                }
            }
        }
    }
    #endregion

    #region CUSTOM_FUNCTIONS
    public void GetDamage(float damageAmount)
    {
        _healthController.GetDamage(damageAmount);
    }
    private void CheckBorders()
    {
        Vector3 borderAiming = Quaternion.AngleAxis(-40, transform.forward) * transform.right;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, borderAiming, 2, _bordersLayerMask);

        if (rayInfo == false)
        {
            Turn();
        }
    }
    public void Shoot()
    {
        _weapon.Shoot(false);
    }
    void Turn()
    {
        transform.Rotate(0, 180, 0);
        _isLookingForward = !_isLookingForward;
    }
    private void Move()
    {
        if (_isLookingForward) _rigidbody.position += new Vector2(MovementSpeed * Time.deltaTime, 0);
        else _rigidbody.position -= new Vector2(MovementSpeed * Time.deltaTime, 0);
    }
    public bool IsPlayerInSight()
    {
        bool result;
        float castDistance = _SightDistance;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _SightDistance, _playerLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                result = true;
                _isAttacking = true;
                _isMoving = false;
            }
            else
            {
                result = false;
                _isAttacking = false;
                _isMoving = true;
            }
        }
        else
        {
            result = false;
            _isAttacking = false;
            _isMoving = true;
        }
        return result;
    }
    #endregion

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 aiming = Quaternion.AngleAxis(-40, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, aiming * 2);

        Gizmos.color = Color.red;
        Vector3 playerDetection = Quaternion.AngleAxis(-40, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, transform.right * _SightDistance);
    }
}
