using UnityEngine;

public class PrefabBullet : MonoBehaviour
{
    #region PRIVATE_PROPERTIES
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private AudioSource sound; //A Sacar
    private bool _isDestroyed;
    private bool _isFromPlayer;
    private float _speed;
    private float _lifeSpawn;
    private float _damageAmount;
    #endregion

    #region PUBLIC_PROPERTIES
    public bool IsFromPlayer => _isFromPlayer;
    public float DamageAmount => _damageAmount;
    #endregion

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        sound = GetComponent<AudioSource>();
    }

    void Start()
    {
        ColliderResize();
        //_speed = 15f;
        sound.Play();
        _lifeSpawn = 0;
        _isDestroyed = false;
    }

    void Update()
    {
        if (!GameManager.isGamePaused)
        {
            _lifeSpawn += Time.deltaTime;

            if (!_spriteRenderer.isVisible || _isDestroyed)
            {
                Destroy(_boxCollider);
                _spriteRenderer.color = new Vector4(0, 0, 0, 0);

                if (_lifeSpawn >= 1.25f)
                {
                    Destroy(this.gameObject);
                }
            }
            gameObject.transform.position += transform.right * _speed * Time.deltaTime;
        }
    }

    private void ColliderResize()
    {
        Vector2 colliderSize = _spriteRenderer.bounds.size;
        _boxCollider.size = colliderSize;
    }

    public void DestroyBullet()
    {
        _isDestroyed = true;
    }

    public void SetBullet(bool isFromPlayer, float damageAmount, float speed)
    { 
        _isFromPlayer = isFromPlayer;
        _damageAmount = damageAmount;
        _speed = speed;
        _boxCollider.enabled = true;
    }
}

