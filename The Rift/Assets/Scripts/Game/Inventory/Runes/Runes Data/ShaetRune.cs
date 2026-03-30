using Entity.Runes;
using UnityEngine;

namespace Game.Inventory.Runes.Runes_Data
{
    public class ShaetRune : RuneData
    {
        public float NonSpecialSlotBobus = 0.30f;

        
        public override float GetStatMultiplier(Influence parameter, RuneContext context)
        {
            if (parameter == Influence.Cooldown && context.CurrentSlotType != RuneSlotsType.Special)
                return NonSpecialSlotBobus;

            return 0;
        }
        
        public override void OnWeaponHit(RuneContext context)
        {
            if (context.CurrentSlotType == RuneSlotsType.Special)
            {
                
            }
        }
        
        public virtual void OnTick(RuneContext context) { }

    }
}