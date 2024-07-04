using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : BaseCharacter, IDamageable, IHealeable, IShooter
{
    #region PUBLIC_PROPERTIES
    public HealthController HealthController => _healthController;
    public BaseWeapon BaseWeapon => _currentWeapon;
    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField] private BaseWeapon _currentWeapon;
    private HealthController _healthController;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidBody;
    private WeaponChanger _weaponChanger;
    private AudioSource _audioSource;
    #endregion

    #region UNITY_FUNCTIONS
    void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _weaponChanger = GetComponent<WeaponChanger>();
        _audioSource = GetComponent<AudioSource>();
        _isPlayer = GetComponent<Player>() == null ? false : true;
    }
    private void Start()
    {
        _healthController.Initialize(MaxLife*GameManager.Instance.DifficultyLevel.PlayerMaxHealth);
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        UI_Controller.Instance.AddObservable(HealthController, UI_ElementType.PlayerHealth);
        UI_Controller.Instance.AddObservable(HealthController, UI_ElementType.GameOverScreen);
        GameManager.Instance.AddObservable(HealthController, EventType.GameOver);
    }
    void Update()
    {
        if (GameManager.IsGamePaused) return;

        WeapongChange();

        if (_spriteRenderer.color != Color.white)
        {
            _spriteRenderer.color += new Color(0, 1, 1, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 11)
        {
            GetDamage(15);
            Destroy(other.gameObject);
            PlayHurtClip();
        }

        if (other.gameObject.GetComponent<BoxScript>() != null)
        {
            if (!other.gameObject.GetComponent<BoxScript>().isGrounded)
            {
                GetDamage(30);
                Destroy(other.gameObject);
                PlayHurtClip();
            }
        }

        if (other.gameObject.CompareTag("Healing"))
        {
            GetHealing(25);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PrefabBullet>() != null)
        {
            if (!other.gameObject.GetComponent<PrefabBullet>().IsFromPlayer)
            {
                GetDamage(5);
                _spriteRenderer.color = Color.red;
                other.gameObject.GetComponent<PrefabBullet>().DestroyBullet();
                PlayHurtClip();
            }
        }
    }
    private void OnDisable()
    {
        UI_Controller.Instance.RemoveObservable(HealthController, UI_ElementType.PlayerHealth);
    }
    #endregion

    #region CUSTOM_FUNCTIONS
    /// <summary>
    /// Rezises the collider with the sprite dimensions
    /// </summary>
    private void ColliderResize()
    {
        Vector2 colliderSize = _spriteRenderer.bounds.size;
        _boxCollider.size = colliderSize;
    }
    public void GetDamage(float damageAmount)
    {
        HealthController.GetDamage(damageAmount);
        if (HealthController.CurrentLife <= 0)
        {
            GameManager.Instance.OnNotify(ObserverMessages.GameOver);
        }
    }
    public void GetHealing(float healAmount)
    {
        HealthController.GetHealing(healAmount);
    }
    private void WeapongChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetNewWeapon(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetNewWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetNewWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GetNewWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GetNewWeapon(4);
        }
    }
    void GetNewWeapon(int weaponKey)
    {
        Vector3 weaponPos = _currentWeapon.transform.position;
        Destroy(_currentWeapon.gameObject);
        GameObject newWeapon = Instantiate(_weaponChanger.RequestWeapon(weaponKey), _currentWeapon.transform.position, this.transform.rotation);
        _currentWeapon = newWeapon.GetComponent<BaseWeapon>();
        newWeapon.transform.position = weaponPos;
        newWeapon.transform.parent = this.transform;
    }
    public void Shoot()
    {
        _currentWeapon.Shoot(true);
    }

    private void PlayHurtClip()
    { 
        _audioSource.Play();
    }
    #endregion
}
