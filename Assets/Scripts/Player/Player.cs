using UnityEngine;

public class Player : BaseCharacter, IDamageable, IHealeable, IShooter
{
    #region PUBLIC_PROPERTIES
    public HealthController HealthController => _healthController;
    public BaseWeapon BaseWeapon => _currentWeapon;
    public bool IsPlayer() => _isPlayer;
    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField] private BaseWeapon _currentWeapon;
    private HealthController _healthController = new HealthController();
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidBody;
    #endregion

    #region UNITY_FUNCTIONS
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isPlayer = GetComponent<Player>() == null ? false : true;
    }

    private void Start()
    {
        _healthController.Initialize(MaxLife);
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        ColliderResize();
        if (GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this.gameObject;
        }

        GameManager.Instance.gameOver = false;
    }

    void Update()
    {
        if (HealthController.CurrentLife <= 0)
        {
            EventManager.Instance.PlayerDefeated();
            GameManager.Instance.gameOver = true;
        }

        if (_spriteRenderer.color != Color.white)
        {
            _spriteRenderer.color += new Color(0, 1, 1, 0) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
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
        }

        if (other.gameObject.GetComponent<BoxScript>() != null)
        {
            if (!other.gameObject.GetComponent<BoxScript>().isGrounded)
            {
                GetDamage(30);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Healing"))
        {
            GetHealing(25);
            EventManager.Instance.HpChanged();
            Destroy(other.gameObject);
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.GetComponent<PrefabBullet>() != null)
        {
            if (!other.gameObject.GetComponent<PrefabBullet>().IsFromPlayer)
            {
                GetDamage(5);
                _spriteRenderer.color = Color.red;
                EventManager.Instance.HpChanged();
                other.gameObject.GetComponent<PrefabBullet>().DestroyBullet();
            }
       }
    }

    public void GetDamage(float damageAmount)
    {
        HealthController.GetDamage(damageAmount);
    }

    public void GetHealing(float healAmount)
    {
        HealthController.GetHealing(healAmount);
    }

    private void PickupWeapon()
    { 
        
    }

    public void Shoot()
    {
        _currentWeapon.Shoot();
    }
    #endregion
}
