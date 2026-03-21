using System;
using System.Collections.Generic;
using Entity.Attacks;
using Game.Inventory.Runes;

namespace Entity.Runes
{
    public class RuneCalculator
    {
        
        public static float GetTotalMultiplier(IEnumerable<RuneData> runes, Influence target, RuneContext context)
        {
            var totalBonus = 0f;
            
            var runePowerMultiplier = 1f;
            foreach (var rune in runes)
                runePowerMultiplier += (rune.GetStatMultiplier(Influence.OtherRunes, context) - 1f);

            foreach (var rune in runes)
            {
                var baseBonus = rune.GetStatMultiplier(target, context) - 1f;
                totalBonus += baseBonus * runePowerMultiplier;
            }
            
            return 1f + totalBonus;
        }
    }
}