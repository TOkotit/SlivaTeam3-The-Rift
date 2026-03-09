using Entity;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorStats", menuName = "Scriptable Objects/WarriorStats")]
public class WarriorStats : ScriptableObject
{
    [Header("Значения зависят от у.е. (класс BC)")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _health;
    [SerializeField] private float _skill1Cooldown;
    [SerializeField] private float _skill2Cooldown;
    
    
    public float Speed => _speed * BC.Speed;
    public float JumpHeight => _jumpHeight * BC.Speed;
    
    public float Damage => _damage * BC.Damage;
    public float AttackSpeed => _attackSpeed *  BC.AtkSpeed;
    public int Health => _health * BC.Health;
    
    public float Skill1Cooldown => _skill1Cooldown * BC.CD;
    public float Skill2Cooldown => _skill2Cooldown * BC.CD;
        
}
