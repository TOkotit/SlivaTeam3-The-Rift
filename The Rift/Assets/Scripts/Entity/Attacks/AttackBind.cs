using System;
using System.Collections.Generic;
using AYellowpaper;
using MainCharacter;
using UnityEngine.InputSystem;
using Utils.MiscClasses;

namespace Entity.Attacks
{
       
    [Serializable]
    public class AttackBind
    {
        public List<InputButton> keys; 
        public InterfaceReference<IAttackProfile> AttackProfile;
        public Weapon weapon;
    }
    
}