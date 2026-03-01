using System;
using System.Collections;
using MainCharacter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[RequireComponent(typeof(CharacterController))]
public class MainCharacterMovement : MonoBehaviour, IControllable
{
    //TODO всё что можно перенести в статы в scriptableObject надо бы перенести
    //TODO RE: Прямое указание параметров в данном скрипте через ScriptableObject было бы нарушением архитектуры
    //Надо будет сделать фабрику чтобы использовать scriptableObject
    
    private CharacterController _controller;
    
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckRadius;

    [Inject]
    private void Construct(MainCharacter.MainCharacter mainCharacter)
    {
        _mainCharacterModel =  mainCharacter.MainCharacterModel;
        _stamina =  _mainCharacterModel.Stamina;
    }
    
    MainCharacterModel _mainCharacterModel;
    Stamina _stamina;
    
    
    
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private Vector3 _gravity = Vector3.down * 9.832f;
    private int currentWallJumpCount;
    private bool _canDash = true;
    public Vector3 MoveDirection {get => _moveDirection; set => _moveDirection = value; }
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    
    private void MoveInternal()
    {
        _controller.Move(_moveDirection * (_mainCharacterModel.Speed * Time.fixedDeltaTime));
    }
    private void FixedUpdate()
    {
        MoveInternal();
        Inert();
        if (IsGrounded())
        {
            _velocity.y = -2f;
            _stamina.RestoreStamina(5);
            currentWallJumpCount = _mainCharacterModel.WallJumpCount;
        }
        
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }
    
    private bool IsUpToAWall()
    {
        return Physics.CheckCapsule(groundCheck.position, new Vector3(wallCheckRadius, 0.05f, wallCheckRadius), groundMask);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _velocity.y += _mainCharacterModel.JumpHeight;
        }
        else if (IsUpToAWall() && currentWallJumpCount > 0)
        {
            if(_stamina.SpendStamina(_mainCharacterModel.WallJumpCost))
            {
                _velocity.y += _mainCharacterModel.JumpHeight / 2;
                currentWallJumpCount--;
            }
        }
    }
    
    private void Inert()
    {
        _velocity += _gravity * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }

    public void Dash()
    {
        Debug.Log("Dash");
        if (_stamina.SpendStamina(_mainCharacterModel.DashCost) && _canDash)
        {
            Debug.Log(_stamina.CurrentStamina);
            Vector3 dashDirection = _moveDirection;
            if (dashDirection == Vector3.zero) dashDirection = transform.forward;
            StartCoroutine(DashRoutine(dashDirection));
        }
    }

    private IEnumerator DashRoutine(Vector3 dashDirection)
    {
        _canDash = false;
        var dashVector = _mainCharacterModel.DashSpeed * dashDirection;
        _velocity += dashVector;
        yield return new WaitForSeconds(_mainCharacterModel.DashTime); 
        _velocity -= dashVector;
        yield return new WaitForSeconds(_mainCharacterModel.DashCooldown); 
        _canDash = true;
    }
}
