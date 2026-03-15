using System;
using TMPro;
using Unity.Behavior;
using UnityEngine;
using VContainer;


namespace Entity.Enemy.WarriorEnemy
{
    public class Warrior : Enemy
    {
        [SerializeField] private TextMeshProUGUI healthText;
        
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private EnemyAttackController _attackController;
        
        private BehaviorGraphAgent behaviorTree;
        
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
        
        [Inject]
        private void SetupModel(WarriorStats stats)
        {
            _enemyModel.Health = new();
            
            _enemyModel.PatrolSpeed = stats.PatrolSpeed;
            _enemyModel.ChaseSpeed = stats.ChaseSpeed;
            _enemyModel.JumpHeight = stats.JumpHeight;
            
            _enemyModel.ChasingToDistance = stats.ChasingToDistance;
            _enemyModel.AttackDistance = stats.AttackDistance;
            
            _enemyModel.Health.SetMaxHealth(stats.Health, true);
            _enemyModel.Damage = stats.Damage;
            _enemyModel.AttackSpeed = stats.AttackSpeed;
            _enemyModel.Skill1Cooldown = stats.Skill1Cooldown;
            _enemyModel.Skill2Cooldown = stats.Skill2Cooldown;
            
        }
        
        public new void Start()
        {
            base.Start();

            InitializeBlackboard();
            
            UpdateHealthText(Damagable.Health.CurrentHealth);
            Damagable.Health.OnHealthChanged += UpdateHealthText;
        }

        public new void OnDestroy()
        {

            Damagable.Health.OnHealthChanged -= UpdateHealthText;
            
            base.OnDestroy();
        }
        
        //Статы которые нужны для behavior agent
        void InitializeBlackboard()
        {
            behaviorTree.SetVariableValue("PatrolSpeed", _enemyModel.PatrolSpeed);
            behaviorTree.SetVariableValue("ChaseSpeed", _enemyModel.ChaseSpeed);
            behaviorTree.SetVariableValue("ChasingToDistance", _enemyModel.ChasingToDistance);
            behaviorTree.SetVariableValue("AttackDistance", _enemyModel.AttackDistance);
            
            behaviorTree.SetVariableValue("AttackSpeed", _enemyModel.AttackSpeed);
        }
    }
}