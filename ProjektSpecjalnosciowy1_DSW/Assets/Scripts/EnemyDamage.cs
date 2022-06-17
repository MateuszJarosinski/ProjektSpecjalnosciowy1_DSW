using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    protected Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (!_health.IsDead)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Health>().TakeDamege(damage);
            }   
        }
    }
}
