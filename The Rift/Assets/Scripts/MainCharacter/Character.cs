using System;
using UnityEngine;
using VContainer.Unity;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _jumpHeight;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private Vector3 _gravity = Vector3.down * 9.832f;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Move(Vector3 direction)
    {
        _controller.Move(direction * (_speed * Time.fixedDeltaTime));
    }

    private void Update()
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move(_moveDirection);
        Inert();
        if (IsGrounded())
        {
            _velocity.y = -2f;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
    }

    private void Jump()
    {
        _velocity.y = _jumpHeight;
    }
    
    private void Inert()
    {
        _velocity += _gravity * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }
}
