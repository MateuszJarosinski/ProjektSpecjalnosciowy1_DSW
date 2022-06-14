using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private int _currentAttack;
    private float _timeSinceAttack;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayers;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _playerMovement.CanAttack())
        {
            Attack();
        }
        
        _timeSinceAttack += Time.deltaTime;
    }

    private void Attack()
    {
        if (_timeSinceAttack > 0.6f)
        {
            _currentAttack++;
        
            if (_currentAttack > 4)
                _currentAttack = 1;
        
            if (_timeSinceAttack > 2.0f)
                _currentAttack = 1;
        
            _animator.Play("PlayerAttack" + _currentAttack);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log(enemy.name);
            }
        
            _timeSinceAttack = 0.0f;   
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
