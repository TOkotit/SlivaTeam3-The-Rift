using System.Collections.Generic;

namespace MainCharacter
{
    public class Health
    {
        private int healthPoints;
        public int HealthPoints => healthPoints;
        private Dictionary<Enums.DamageTypes, float> _vulnerabilities = new Dictionary<Enums.DamageTypes, float>();
        public int TakeDamage(int damage, Enums.DamageTypes damageType)
        {
            healthPoints -= (int)(damage * _vulnerabilities[damageType]);
            
            return healthPoints;
        }
    }
}