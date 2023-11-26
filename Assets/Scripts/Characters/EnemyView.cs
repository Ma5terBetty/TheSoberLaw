using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    Enemy _enemyController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyController = GetComponent<Enemy>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_enemyController.IsGrounded)
        {
            _animator.SetBool("IsShooting", false);
            _animator.SetBool("IsMoving", false);
        }
        _animator.SetBool("IsShooting", _enemyController.IsAttacking);
        _animator.SetBool("IsMoving", _enemyController.IsMoving);

        if (_spriteRenderer.color != Color.white)
        {
            _spriteRenderer.color += new Color(0, 1, 1, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.gameObject.GetComponent<PrefabBullet>();
        if (bullet != null)
        {
            if (bullet.IsFromPlayer)
            {
                _spriteRenderer.color = Color.red;
                collision.gameObject.GetComponent<PrefabBullet>().DestroyBullet();
            }
        }
    }
}
