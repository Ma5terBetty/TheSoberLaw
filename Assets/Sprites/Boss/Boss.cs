using UnityEngine;

public class Boss : BaseCharacter, IDamageable, IShooter
{
    #region PRIVATE_PROPERTIES
    private HealthController _healthController = new HealthController();
    [SerializeField] private BaseWeapon _currentWeapon;
    [SerializeField] private GameObject _whiskeyGO;
    private Rigidbody2D _rigidbody;
    private WeaponChanger _weaponChanger;

    private int _bossLevel;
    private int _whiskeyCounter;

    private bool _isLookingForward;
    private bool _isAttacking;
    private bool _isThrowingWhisky;

    private float _whiskeyTimer = 0;
    private float _whiskeyCooldown = 0;
    #endregion

    #region PUBLIC_PROPERTIES
    public HealthController HealthController => _healthController;
    public BaseWeapon BaseWeapon => _currentWeapon;
    public bool IsPlayer() => _isPlayer;
    public bool IsAttacking => _isAttacking;
    #endregion

    #region UNITY_FUNCTIONS
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isPlayer = GetComponent<Player>() == null ? false : true;
        _weaponChanger = GetComponent<WeaponChanger>();
    }
    private void Start()
    {
        _healthController.Initialize(_characterStats.MaxLife);
        _bossLevel = 0;
        _isAttacking = true;
        _isLookingForward = true;
    }
    private void Update()
    {
        if (GameManager.IsGamePaused) return;

        if (_isThrowingWhisky)
        {
            WhiskeyAttack();
        }
        if (_isAttacking)
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.IsGamePaused) return;

        if (_isAttacking)
        {
            Move(_bossLevel);
        }        
    }
    #endregion

    #region COLLISION_AND_TRIGGERS
    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.gameObject.GetComponent<PrefabBullet>();
        if (bullet != null)
        {
            if (bullet.IsFromPlayer)
            {
                GetDamage(bullet.DamageAmount);
                other.gameObject.GetComponent<PrefabBullet>().DestroyBullet();
            }
        }
    }
    #endregion

    #region OTHER_FUNCTIONS
    public void GetDamage(float damageAmount)
    {
        _healthController.GetDamage(damageAmount);
        if (HealthController.CurrentLife <= (_characterStats.MaxLife / 3)*2)
        {
            _bossLevel = 1;
            ChangeWeapon(_bossLevel);
        }
        if (HealthController.CurrentLife <= _characterStats.MaxLife / 3)
        {
            _bossLevel = 2;
            ChangeWeapon(_bossLevel);
        }
        if (HealthController.CurrentLife <= 0)
        { 
            GameManager.Instance.isBossDefeated = true;
            Destroy(this.gameObject);
        }
    }
    public void Shoot()
    {
        _currentWeapon.Shoot(_isPlayer);
    }
    void WhiskeyAttack()
    {
        _whiskeyCooldown += Time.deltaTime;

        if (_whiskeyCooldown >= (1.2f - (0.3*_bossLevel)))
        {
            Instantiate(_whiskeyGO, transform.position, transform.rotation);
            _whiskeyCooldown = 0;
            _whiskeyCounter++;

            if (_whiskeyCounter >= 3 + _bossLevel)
            {
                _isThrowingWhisky = false;
                _isAttacking = true;
                _whiskeyCounter = 0;
                Turn();
            }
        }
    }
    private void ChangeWeapon(int weaponId)
    {
        Vector3 weaponPos = _currentWeapon.transform.position;
        Destroy(_currentWeapon.gameObject);
        GameObject newWeapon = Instantiate(_weaponChanger.RequestWeapon(weaponId), _currentWeapon.transform.position, this.transform.rotation);
        _currentWeapon = newWeapon.GetComponent<BaseWeapon>();
        newWeapon.transform.position = weaponPos;
        newWeapon.transform.parent = this.transform;
    }
    public void Turn()
    {
        transform.Rotate(0, 180, 0);
        _isLookingForward = !_isLookingForward;
    }
    void Move(float speedVariation)
    {
        speedVariation += MovementSpeed;
        if (_isLookingForward) _rigidbody.position += new Vector2(speedVariation * Time.deltaTime, 0);
        else _rigidbody.position -= new Vector2(speedVariation * Time.deltaTime, 0);
    }

    public void TurnOnWhiskey()
    {
        _isThrowingWhisky = true;
        _isAttacking = false;
    }
    #endregion
}
