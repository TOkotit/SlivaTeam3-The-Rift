namespace Game.Inventory.Runes
{
    public struct RuneContext
    {
        
        //тут будет добавляться по мере необходимости
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