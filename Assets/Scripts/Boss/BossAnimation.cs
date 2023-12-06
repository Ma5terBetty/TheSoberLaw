using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class BossAnimation : MonoBehaviour
{
    private Animator _animator;
    private Boss _boss;

    private void Awake()
    {
        _boss = GetComponent<Boss>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _animator.SetBool("IsAttacking", _boss.IsAttacking);   
    }
}
