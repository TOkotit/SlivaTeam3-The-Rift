using System;
using System.Collections.Generic;
using MainCharacter;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
       
    [Serializable]
    public class AttackBind
    {
        public List<Key> keys; 
        public IAttackProfile AttackProfile;
        public Weapon weapon;
    }
    
}