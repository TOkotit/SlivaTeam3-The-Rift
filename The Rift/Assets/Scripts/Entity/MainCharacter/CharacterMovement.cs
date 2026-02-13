using System;
using MainCharacter;
using UnityEngine;
using VContainer.Unity;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour, IControllable
{
    //Компонент который можно цеплять на объекты которые должны двигаться, например персонажей
    private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _jumpHeight;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private Vector3 _gravity = Vector3.down * 9.832f;
    public Vector3 MoveDirection {get => _moveDirection; set => _moveDirection = value; }
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    private void MoveInternal()
    {
        _controller.Move(_moveDirection * (_speed * Time.fixedDeltaTime));
    }
    private void FixedUpdate()
    {
        MoveInternal();
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

    public void Jump()
    {
        if (IsGrounded())
        {
            _velocity.y += _jumpHeight;
        }
    }
    
    private void Inert()
    {
        _velocity += _gravity * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }
}
