using System;
using System.Collections.Generic;
using Entity.Attacks;
using Game.Inventory.Runes;

namespace Entity.Runes
{
    public class RuneCalculator
    {
        
        public static float CalculateStat(float baseValue, Influence influence, List<RuneSlot> slots, Func<RuneSlotsType, RuneContext> contextFactory)
        {
            var totalBonus = 0f;
            var runePowerMultiplier = 1f;

            foreach (var slot in slots)
            {
                if (slot.IsEmpty) continue;
                
                var context = contextFactory.Invoke(slot.SlotType);
                runePowerMultiplier += (slot.EquippedRune.GetStatMultiplier(Influence.OtherRunes, context) - 1f);
            }

            foreach (var slot in slots)
            {
                if (slot.IsEmpty) continue;

                var context = contextFactory.Invoke(slot.SlotType);
                var baseBonus = slot.EquippedRune.GetStatMultiplier(influence, context) - 1f;
                totalBonus += baseBonus * runePowerMultiplier;
            }

            return baseValue * (1f + totalBonus);
        }
    }
}