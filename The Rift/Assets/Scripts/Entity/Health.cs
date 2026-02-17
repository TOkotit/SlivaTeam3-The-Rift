using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace MainCharacter
{
    public class Health
    {
        private int _maxHealth = 100;
        private int _currentHealth;
        private Dictionary<Enums.DamageTypes, float> _vulnerabilities = new Dictionary<Enums.DamageTypes, float>();

        public int CurrentHealth
        {
            get { return _currentHealth; }
            private set
            {
                _currentHealth = value;
                if (_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
                if (_currentHealth <= 0) _currentHealth = 0;
                OnDeath?.Invoke();
            }
        }
        public IReadOnlyDictionary<Enums.DamageTypes, float> Vulnerabilities => _vulnerabilities;

        public event Action<int> OnHealthChanged;
        public event Action OnDeath;
        
        public int TakeDamage(int damage, Enums.DamageTypes damageType)
        {
            if (damage <= 0) return 0;
            if (!_vulnerabilities.TryGetValue(damageType, out float coefficient))
                coefficient = 1f;
            var total = (int)(damage * coefficient);
            CurrentHealth -= total;
            OnHealthChanged?.Invoke(CurrentHealth);
            //Возврат количества здоровья на случай визуализации 
            return _currentHealth;
        }

        public int Heal(int heal)
        {
            if (heal <= 0) return 0;
            CurrentHealth += heal;
            OnHealthChanged?.Invoke(CurrentHealth);
            //анналогично
            return heal;
        }
        public Health(int maxHP)
        {
            _maxHealth = maxHP;
            _currentHealth = _maxHealth;
        }
        public void SetVulnerability(Enums.DamageTypes damageType, float vulnerability)
        {
            _vulnerabilities[damageType] = vulnerability;
        }

        public void AddVulnerability(Enums.DamageTypes damageType, float vulnerability)
        {
            if (_vulnerabilities.ContainsKey(damageType))
                _vulnerabilities[damageType] += vulnerability;
            else
                _vulnerabilities[damageType] = 1f + vulnerability;
        }

        public void SetMaxHealth(int newMaxHealth)
        {
            _maxHealth = newMaxHealth;
        }
        
    }
}