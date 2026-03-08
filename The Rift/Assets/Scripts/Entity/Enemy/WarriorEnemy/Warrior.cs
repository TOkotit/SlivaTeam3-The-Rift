using System;
using TMPro;
using UnityEngine;

namespace Entity.Enemy.WarriorEnemy
{
    public class Warrior : Enemy
    {
        [SerializeField] private TextMeshProUGUI healthText;
        
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private EnemyAttackController _attackController;
        
        public EnemyAttackController AttackController
        {
            get => _attackController;
            set => _attackController = value;
        }

        public TargetDetector Detector
        {
            get => _targetDetector;
            set => _targetDetector = value;
        }

        public void UpdateHealthText(int health)
        {
            healthText.text = $"Health: {health}";
        }
        
        public new void Start()
        {
            base.Start();
            
            // Damagable.Health.OnHealthChanged += UpdateHealthText;
        }

        public new void OnDestroy()
        {

            // Damagable.Health.OnHealthChanged -= UpdateHealthText;
            
            base.OnDestroy();
        }
    }
}