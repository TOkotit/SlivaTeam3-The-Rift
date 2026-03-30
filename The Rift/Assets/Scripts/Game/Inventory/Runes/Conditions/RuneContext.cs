using UnityEngine;

namespace Game.Inventory.Runes
{
    public struct RuneContext
    {
        public GameObject Owner;       
        public GameObject Target;      
        public Vector3 HitPoint;       
    
        // Твои текущие параметры
        public float CurrentDurabilityPercent; 
        public float HealthPercent;
        public float TimeSinceLastHit;
        public float CharacterSpeed;
        public EquipmentType EquipType;
    }
    
    public enum EquipmentType
    {
        Weapon,
        Armor,
    }
}