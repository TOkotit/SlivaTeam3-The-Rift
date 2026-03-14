using UnityEngine;

namespace Game.Inventory.Runes
{
    [CreateAssetMenu(fileName = "New Recently Hit Condition", menuName = "Runes/Conditions/Recently Hit")]
    public class RecentlyHitCondition : RuneCondition
    {
        public float BuffDuration = 2f; 

        public override bool IsMet(RuneContext context)
        {
            return context.TimeSinceLastHit <= BuffDuration;
        }
    }
}