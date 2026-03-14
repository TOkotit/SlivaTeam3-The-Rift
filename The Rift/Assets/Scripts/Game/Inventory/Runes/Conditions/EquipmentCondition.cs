using UnityEngine;

namespace Game.Inventory.Runes
{
    [CreateAssetMenu(fileName = "New Equipment Condition", menuName = "Runes/Conditions/Equipment")]

    public class EquipmentCondition : RuneCondition
    {
        [Tooltip("На каком типе предмета должен работать этот баф?")]
        public EquipmentType RequiredType;

        public override bool IsMet(RuneContext context)
        {
            return context.EquipType == RequiredType;
        }
    }
}