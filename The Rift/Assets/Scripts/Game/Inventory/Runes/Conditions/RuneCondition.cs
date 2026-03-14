using UnityEngine;

namespace Game.Inventory.Runes
{
    public abstract class RuneCondition : ScriptableObject
    {
        public abstract bool IsMet(RuneContext context);
    }
}