using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    public float colliderDistance;
    [SerializeField] private int attackDamage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask layerMask;
    private float cooldownTimer = Mathf.Infinity;
    private Animator _animator;
    private Health _playerHealth;

    private EnemyPatrol _enemyPatrol;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = transform.parent.GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (CanSeePlayer())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                FindObjectOfType<AudioManager>().Play("sword");
                _animator.SetTrigger("isAttacking");
            }  
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !CanSeePlayer();
        }
    }

    private bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*attackRange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, layerMask);

        if (hit.collider != null)
        {
            _playerHealth = hit.transform.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*attackRange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (CanSeePlayer())
        {
            _playerHealth.TakeDamege(attackDamage);
        }
    }
}
