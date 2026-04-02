using Entity;
using UnityEngine;

[CreateAssetMenu(fileName = "SprinterStats", menuName = "Scriptable Objects/SprinterStats")]
public class SprinterStats : ScriptableObject
{
    [Header("Значения зависят от у.е. (класс BC)")] 
    
    [SerializeField] private SprinterAiStates _startingState;
    
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _jumpHeight;

    [SerializeField] private float _chasingToDistance;
    [SerializeField] private float _attackDistance;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackChargeTime;
    [SerializeField] private float _parryTime;
    
    [SerializeField] private int _health;
    [SerializeField] private float _skill1Cooldown;
    [SerializeField] private float _skill2Cooldown;
    
    
    public float PatrolSpeed => _patrolSpeed * BC.Speed;
    public float ChaseSpeed => _chaseSpeed * BC.Speed;
    public float JumpHeight => _jumpHeight * BC.Speed;
    
    public float ChasingToDistance => _chasingToDistance;
    public float AttackDistance => _attackDistance;
    
    public float Damage => _damage * BC.Damage;
    public float AttackSpeed => _attackSpeed *  BC.AtkSpeed;
    public float AttackChargeTime => _attackChargeTime;
    
    public float ParryTime => _parryTime;
    public int Health => _health * BC.Health;
    
    public float Skill1Cooldown => _skill1Cooldown * BC.CD;
    public float Skill2Cooldown => _skill2Cooldown * BC.CD;
        
}
