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
            var runePowerMultiplier = 1f;
            
            
            foreach (var rune in runes)
                foreach (var mod in rune.Modifiers)
                    if (mod.Parameter == Influence.OtherRunes && AreConditionsMet(mod, context))
                        runePowerMultiplier += (mod.Coefficient - 1f);
            
            var totalBonus = 0f;
            foreach (var rune in runes)
                foreach (var mod in rune.Modifiers)
                    if (mod.Parameter == target && AreConditionsMet(mod, context))
                    {
                        var baseBonus = mod.Coefficient - 1f;
                        totalBonus += baseBonus * runePowerMultiplier;
                    }
            
            return 1f + totalBonus;
        }
        
        private static bool AreConditionsMet(RuneModifier mod, RuneContext context)
        {
            if (mod.Conditions == null || mod.Conditions.Count == 0) return true;
    
            foreach (var condition in mod.Conditions)
                if (!condition.IsMet(context)) return false;
            return true;
        }
    }
}