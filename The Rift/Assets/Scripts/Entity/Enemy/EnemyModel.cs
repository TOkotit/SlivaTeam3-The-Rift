using Enums;
using UnityEngine.TextCore.Text;

namespace Entity.Enemy
{
    public class EnemyModel : DamagableModel
    {
        private float _speed;
        private float _jumpHeight;
        private float _damage;
        private float _attackSpeed;
        private float _skill1Cooldown;
        private float _skill2Cooldown;

        public EnemyModel()
        {
            _team = Teams.Enemy;
        }

        public float Speed {get => _speed; set => _speed = value; }
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
    }
}