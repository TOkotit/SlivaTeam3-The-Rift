using System;
using Entity.Runes;
using UnityEngine;

namespace Game.Inventory.Runes
{
    [Serializable]
    public class RuneSlot 
    {
        public RuneSlotsType SlotType { get; }
        public RuneData EquippedRune;
        
        public bool IsEmpty => EquippedRune == null;

        public RuneSlot(RuneSlotsType type)
        {
            SlotType = type;
        }
    }
}