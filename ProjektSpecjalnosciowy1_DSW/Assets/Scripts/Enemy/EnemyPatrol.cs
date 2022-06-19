using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    public EnemyCombat _enemyCombat;
    private Vector3 initScale;
    private bool _movingLeft;

    [SerializeField] private float idleDuration;

    [SerializeField] private Animator animator;

    private float _idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    
    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);   
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        animator.SetBool("isWalking", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
        
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.x);
    }

    private void ChangeDirection()
    {
        animator.SetBool("isWalking", false);
        _idleTimer += Time.deltaTime;

        if (_idleTimer > idleDuration)
        {
            _movingLeft = !_movingLeft;
            _enemyCombat.colliderDistance *= -1;   
        }
    }
}
