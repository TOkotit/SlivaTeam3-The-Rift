using System;
using System.Collections.Generic;
using Game.Inventory.Runes;
using UnityEngine;

namespace Entity.Runes
{
    public class RuneData : ScriptableObject
    {
        public string runeName;
        public Sprite icon;
        public RuneType Rune;
        [Tooltip("Список всех параметров, которые меняет эта руна")]
        public virtual float GetStatMultiplier(Influence parameter, RuneContext context) => 1f;
        
        public virtual void OnWeaponHit(RuneContext context) { }
        
        public virtual void OnArmorTakeDamage(RuneContext context) { }
        
        public virtual void OnTick(RuneContext context) { }
    }
}