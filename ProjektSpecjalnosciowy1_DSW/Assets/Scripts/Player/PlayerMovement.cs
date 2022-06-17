using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private float _horizontalInput;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(_horizontalInput * movementSpeed, _rigidbody.velocity.y);

        //Flip player sprite
        if (_horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (_horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (Input.GetKey(KeyCode.Z) && IsGrounded())
        {
            Jump();
        }
        
        _animator.SetBool("isRunning", _horizontalInput != 0);
        _animator.SetBool("isGrounded", IsGrounded());
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        _animator.SetTrigger("jumped");
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 1f, _groundLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return _horizontalInput == 0 && IsGrounded();
    }
}
