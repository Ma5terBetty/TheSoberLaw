using System;
using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : BaseCharacter, IDamageable, IShooter
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private BaseWeapon _weapon;
    private HealthController _healthController;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    [SerializeField] private float _SightDistance = 3f;
    [SerializeField] private float _patrolDistance;
    [SerializeField] private bool _isPatrolling;
    [SerializeField] private LayerMask _bordersLayerMask;
    [SerializeField] private LayerMask _scenarioLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private GameObject _health;
    private bool _isAttacking;
    private bool _isGrounded;
    private bool _isMoving;
    private bool _isLookingForward;
    private Vector3 _initialPosition;
    #endregion

    #region PUBLIC_PROPERTIES
    public HealthController HealthController => _healthController;
    public BaseWeapon BaseWeapon => _weapon;
    public bool IsAttacking => _isAttacking;
    public bool IsGrounded => _isGrounded;
    public bool IsMoving => _isMoving;
    #endregion

    #region UNITY_FUNCTIONS
    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _isPlayer = GetComponent<Player>() == null ? false : true;
    }
    void Start()
    {
        _healthController.Initialize(MaxLife * GameManager.Instance.DifficultyLevel.EnemiesMaxLife);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        _isMoving = true;
        _isLookingForward = true;
        _isGrounded = false;
        _initialPosition = transform.position;
        if (_patrolDistance > 0) _isPatrolling = true;

        UI_Controller.Instance.AddObservable(HealthController, UI_ElementType.ScoreCounter);
    }
    void Update()
    {
        if (GameManager.IsGamePaused || !GameManager.Instance.IsGameplayActive) return;

        if (IsPlayerInSight())
        {
            Shoot();
        }


        //Testing
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetDamage(500);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.IsGamePaused || !GameManager.Instance.IsGameplayActive) return;
        if (!IsGrounded) return;

        if (_isMoving)
        {
            Move();
        }
    }
    private void LateUpdate()
    {
        if (GameManager.IsGamePaused || !GameManager.Instance.IsGameplayActive) return;
        if (!IsGrounded) return;

        if (_isPatrolling)
        {
            if (Vector3.Distance(transform.position, _initialPosition) > _patrolDistance)
            {
                Turn();
                Vector3 aux = (transform.position - _initialPosition).normalized;
                transform.position = transform.position - aux;
            }
        }

        CheckBorders();
    }

    private void OnDisable()
    {
        UI_Controller.Instance.RemoveObservable(HealthController, UI_ElementType.ScoreCounter);
    }
    #endregion

    #region COLLISION_AND_TRIGGERS
    private void OnCollisionStay2D(Collision2D collision)
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
        _audioSource.Play();

        _healthController.GetDamage(damageAmount);

        if (_healthController.CurrentLife <= 0)
        {
            var playerHC = GameManager.Instance.Player.HealthController;
            if (playerHC.CurrentLife <= playerHC.MaxLife / 4)
            {
                Instantiate(_health, transform.position, Quaternion.identity);
            }
            GameManager.Instance.LevelManager.EnemyKilled();
            Destroy(this.gameObject);
        }
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
        float temp = MovementSpeed * Time.deltaTime * GameManager.Instance.DifficultyLevel.EnemiesMovement;
        if (_isLookingForward) _rigidbody.position += new Vector2(temp, 0);
        else _rigidbody.position -= new Vector2(temp, 0);
    }
    public void SetWeapon(GameObject input)
    {
        Vector3 pos = _weapon.transform.position;
        Destroy(_weapon.gameObject);
        GameObject newWeapon = Instantiate(input, _weapon.transform.position, Quaternion.identity);
        _weapon = newWeapon.GetComponent<BaseWeapon>();
        newWeapon.transform.position = pos;
        newWeapon.transform.parent = this.transform;
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
