using System;
using System.Collections.Generic;
using AYellowpaper;
using MainCharacter;
using UnityEngine.InputSystem;

namespace Entity.Attacks
{
       
    [Serializable]
    public class AttackBind
    {
        public List<Key> keys; 
        public InterfaceReference<IAttackProfile> AttackProfile;
        public Weapon weapon;
    }
    
}