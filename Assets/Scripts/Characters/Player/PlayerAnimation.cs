using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private bool _isJumping;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _isJumping = false;
    }

    private void Update()
    {
        var aux = Input.GetAxis("Horizontal");
        if (aux != 0)
        {
            _animator.SetFloat("Speed", Mathf.Abs(aux));

            if (aux > 0)
            { 
                //_spriteRenderer.flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (aux < 0)
            {
                //_spriteRenderer.flipX = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 0 || other.gameObject.layer == 8)
        {
            _isJumping = false;
            _animator.SetBool("IsJumping", _isJumping);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 0)
        {
            _isJumping = false;
            _animator.SetBool("IsJumping", _isJumping);
        }
    }
}
