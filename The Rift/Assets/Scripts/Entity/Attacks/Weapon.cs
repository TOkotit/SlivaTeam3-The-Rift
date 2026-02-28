using System;
using System.Collections.Generic;
using MainCharacter;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
    public class Weapon
    { 
        private WeaponModel _model;
           
        private List<AttackBind> _attacks = new List<AttackBind>();
        public int Durability
        {
            get => _model.CurrentDurability;
            private set
            {
                _model.CurrentDurability = value;
            }
        }

        public float Damage(int damage)
        {
            Durability -= damage;
            return Durability;
        }
        
        public IReadOnlyList<AttackBind> AttackBinds => _attacks;
        public WeaponModel Model => _model;
        
        
        public Weapon(WeaponModel model, List<AttackBind>  attacks)
        {
             _model = model;
             _attacks = attacks;
             foreach (var bind in _attacks)
                 bind.weapon = this;
        }
    }
}