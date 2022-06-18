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
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
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
        if (_timeSinceAttack > 0.5f)
        {
            _currentAttack++;
        
            if (_currentAttack > 4)
                _currentAttack = 1;
        
            if (_timeSinceAttack > 2.0f)
                _currentAttack = 1;
        
            FindObjectOfType<AudioManager>().Play("sword");
            _animator.Play("PlayerAttack" + _currentAttack);
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

    private void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamege(1);
            Debug.Log(enemy.name);
        }
        
        _timeSinceAttack = 0.0f;  
    }

    private void Deactivate()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
