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
    }

    private void FixedUpdate()
    {
        Move(_moveDirection);
        if (IsGrounded())
        {
            _velocity.y = 0;
        }
        Inert();
        
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
    }
    private void Inert()
    {
        _velocity += _gravity * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }
}
