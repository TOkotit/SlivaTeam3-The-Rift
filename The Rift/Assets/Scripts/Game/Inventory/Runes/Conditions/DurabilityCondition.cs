using UnityEngine;

namespace Game.Inventory.Runes
{
    
    [CreateAssetMenu(fileName = "New Durability Condition", menuName = "Runes/Conditions/Durability")]
    public class DurabilityCondition : RuneCondition
    {
        
        public float Threshold = 0.7f;
        public bool MustBeAbove = true;
        
        public override bool IsMet(RuneContext context)
        {
            if (MustBeAbove)
                return context.CurrentDurabilityPercent >= Threshold;
            
            return context.CurrentDurabilityPercent < Threshold;
        }
    }
}