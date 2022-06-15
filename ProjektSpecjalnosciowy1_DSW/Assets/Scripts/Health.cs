using System;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Animator _animator;
    private bool _isDead;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        CurrentHealth = startingHealth;
    }

    public void TakeDamege(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            _animator.SetTrigger("takeDamage");
        }
        else
        {
            if (!_isDead)
            {
                _animator.SetTrigger("die");
                //Player
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;   
                }

                //Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;   
                }

                if (GetComponentInChildren<EnemyCombat>() != null)
                {
                    GetComponentInChildren<EnemyCombat>().enabled = false;   
                }
                _isDead = true;
            }
        }
    }

    public void AddHealth(float health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, startingHealth);
    }
}
