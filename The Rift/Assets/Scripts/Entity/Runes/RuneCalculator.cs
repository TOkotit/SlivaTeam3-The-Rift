using System;
using System.Collections.Generic;
using Entity.Attacks;

namespace Entity.Runes
{
    public class RuneCalculator
    {
        
        public static float GetTotalMultiplier(IEnumerable<RuneData> runes, Influence target)
        {
            var runePowerMultiplier = 1f;
            foreach (var rune in runes)
                foreach (var mod in rune.Modifiers)
                    if (mod.Parameter == Influence.OtherRunes)
                        runePowerMultiplier += (mod.Coefficient - 1f);
            
            var totalBonus = 0f;
            foreach (var rune in runes)
                foreach (var mod in rune.Modifiers)
                    if (mod.Parameter == target)
                    {
                        var baseBonus = mod.Coefficient - 1f;
                        totalBonus += baseBonus * runePowerMultiplier;
                    }
            
            return 1f + totalBonus;
        }
    }
}