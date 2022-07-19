using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private CircleCollider2D _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _faceRight;
    private bool _onGround;
    private float _horizontalMovement;
    private float _checkRadius;

    private void Start()
    {
        _checkRadius = _groundCheck.radius;
        _faceRight = true;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Run();
        Jump();
        Reflect();
    }

    private void Reflect()
    {
        if ((_horizontalMovement > 0 && _faceRight == false ) || (_horizontalMovement < 0 && _faceRight == true))
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = !_faceRight;
        }
    }

    private void Run()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        transform.Translate(_speed * Time.deltaTime * _horizontalMovement, 0, 0);

        if (_horizontalMovement < 0)
            _animator.SetFloat("Speed", _horizontalMovement * -1);
        else
            _animator.SetFloat("Speed", _horizontalMovement);
    }

    private void Jump()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.transform.position, _checkRadius, _groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) == true && _onGround == true)
            _rigidbody.AddForce(Vector2.up * _jumpForce);
    }
}
