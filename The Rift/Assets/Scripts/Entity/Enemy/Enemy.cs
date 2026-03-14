using Enums;
using MainCharacter;
using UnityEngine;
using VContainer;

namespace Entity.Enemy
{
    public class Enemy : Character
    {
        [Inject] private EnemyModel _enemyModel;
        public override DamagableModel Damagable => _enemyModel;
        public EnemyModel EnemyModel => _enemyModel; 
        
        
        [Inject]
        private void SetupModel(WarriorStats stats)
        {
            _enemyModel.Health = new();
            
            _enemyModel.Speed = stats.Speed;
            _enemyModel.JumpHeight = stats.JumpHeight;
            _enemyModel.Health.SetMaxHealth(stats.Health, true);
            _enemyModel.Damage = stats.Damage;
            _enemyModel.AttackSpeed = stats.AttackSpeed;
            _enemyModel.Skill1Cooldown = stats.Skill1Cooldown;
            _enemyModel.Skill2Cooldown = stats.Skill2Cooldown;
            
        }
        
    }
}