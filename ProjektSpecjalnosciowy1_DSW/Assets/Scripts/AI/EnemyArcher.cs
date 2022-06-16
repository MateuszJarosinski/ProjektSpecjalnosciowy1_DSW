using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    
    public float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [SerializeField] private LayerMask layerMask;
    private float cooldownTimer = Mathf.Infinity;
    
    private Animator _animator;
    private Health _playerHealth;

    private EnemyPatrol _enemyPatrol;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    
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
    
    
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*attackRange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void ArcherAttack()
    {
        cooldownTimer = 0;

        arrows[FindFireball()].transform.position = firePoint.position;
        arrows[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        
        return 0;
    }
}
