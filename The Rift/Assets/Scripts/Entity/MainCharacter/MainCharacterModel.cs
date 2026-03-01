using System.Collections.Generic;
using Entity;
using Entity.Attacks;
using NUnit.Framework;

namespace MainCharacter
{
    public class MainCharacterModel : DamagableModel
    {
        private List<WeaponModel>  _weapons;
        public List<WeaponModel> Weapons {get => _weapons; set => _weapons = value; }
        private Stamina _stamina;
        public Stamina  Stamina => _stamina;
        
        private float _speed = 10;
        private float _jumpHeight = 7;
        private int _wallJumpCost = 10;
        private int _dashCost = 20;
        private float _dashSpeed = 50;
        private float _dashTime = 0.1f;
        private float _dashCooldown = 1f;
        private int _wallJumpCount = 1;
        
        public float Speed => _speed;
        public float JumpHeight => _jumpHeight;
        public int WallJumpCost => _wallJumpCost;
        public int DashCost => _dashCost;
        public float DashSpeed => _dashSpeed;
        public float DashTime => _dashTime;
        public float DashCooldown => _dashCooldown;
        public int WallJumpCount => _wallJumpCount;
        public MainCharacterModel(Stamina stamina, Health health)
        {
            Weapons = new List<WeaponModel>();
            _stamina = stamina;
            _health = health;
        }
    }
}