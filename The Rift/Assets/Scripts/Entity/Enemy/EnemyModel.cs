using Enums;
using UnityEngine.TextCore.Text;

namespace Entity.Enemy
{
    public class EnemyModel : DamagableModel
    {
        private EnemyAIStates _startingState;

        private float _patrolSpeed;
        private float _chaseSpeed;
        private float _jumpHeight;

        private float _chasingToDistance;
        private float _attackDistance;

        private float _damage;
        private float _attackSpeed;
        private float _attackChargeTime;
        private float _parryTime;
        
        private int _health;
        private float _skill1Cooldown;
        private float _skill2Cooldown;

        public EnemyModel()
        {
            _team = Teams.Enemy;
        }

        public float PatrolSpeed {get => _patrolSpeed; set => _patrolSpeed = value; }
        public float ChaseSpeed {get => _chaseSpeed; set => _chaseSpeed = value; }
        public float JumpHeight {get => _jumpHeight; set => _jumpHeight = value; }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float AttackSpeed
        {
            get => _attackSpeed;
            set => _attackSpeed = value;
        }

        public float Skill1Cooldown
        {
            get => _skill1Cooldown;
            set => _skill1Cooldown = value;
        }

        public float Skill2Cooldown
        {
            get => _skill2Cooldown;
            set => _skill2Cooldown = value;
        }

        public float ChasingToDistance
        {
            get => _chasingToDistance;
            set => _chasingToDistance = value;
        }

        public float AttackDistance
        {
            get => _attackDistance;
            set => _attackDistance = value;
        }

        public float AttackChargeTime
        {
            get => _attackChargeTime;
            set => _attackChargeTime = value;
        }

        public float ParryTime
        {
            get => _parryTime;
            set => _parryTime = value;
        }
    }
}