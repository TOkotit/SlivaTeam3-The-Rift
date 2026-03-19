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
        [SerializeField] private GameObject _parryArea;
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

        public GameObject ParryArea
        {
            get => _parryArea;
            set => _parryArea = value;
        }


        private void Awake()
        {
            
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
            _enemyModel.AttackChargeTime = stats.AttackChargeTime;
            _enemyModel.ParryTime = stats.ParryTime;
            
            
            _enemyModel.Health.SetMaxHealth(stats.Health, true);
            _enemyModel.Damage = stats.Damage;
            _enemyModel.AttackSpeed = stats.AttackSpeed;
            _enemyModel.Skill1Cooldown = stats.Skill1Cooldown;
            _enemyModel.Skill2Cooldown = stats.Skill2Cooldown;
            
        }
        //Статы которые нужны для behavior agent
        void InitializeBlackboard()
        {
            behaviorTree.SetVariableValue("CurrentState", EnemyAIStates.Idle);
            behaviorTree.SetVariableValue("PatrolSpeed", _enemyModel.PatrolSpeed);
            behaviorTree.SetVariableValue("ChaseSpeed", _enemyModel.ChaseSpeed);
            behaviorTree.SetVariableValue("ChasingToDistance", _enemyModel.ChasingToDistance);
            behaviorTree.SetVariableValue("AttackDistance", _enemyModel.AttackDistance);
            behaviorTree.SetVariableValue("AttackCooldownTime", 1f / _enemyModel.AttackSpeed);
            behaviorTree.SetVariableValue("AttackChargeTime", _enemyModel.AttackChargeTime);
            behaviorTree.SetVariableValue("ParryTime", _enemyModel.ParryTime);
        }
        
        public new void Start()
        {
            base.Start();
            behaviorTree = GetComponent<BehaviorGraphAgent>();
            InitializeBlackboard();
            _attackController.EnemyModel = _enemyModel;
            
            
            UpdateHealthText(Damagable.Health.CurrentHealth);
            Damagable.Health.OnHealthChanged += UpdateHealthText;
            
            // Damagable.Health.OnHealthChanged += DashBack;
            // Damagable.Health.OnHealthChanged += Block;
            Damagable.Health.OnDeath += Die;
        }

        
        
        public new void OnDestroy()
        {
            Damagable.Health.OnDeath -= Die;
            Damagable.Health.OnHealthChanged -= UpdateHealthText;
            
            base.OnDestroy();
        }
        
        private void Die()
        {
            behaviorTree.SetVariableValue("CurrentState", EnemyAIStates.Dead);
            _attackController.AttackQueue.FinishAttack();
            
        }
        
        public void DashBack(int a)
        {
            behaviorTree.SetVariableValue("CurrentState", EnemyAIStates.SpecialAbility1);
        }
        
        public void Block(int a)
        {
            behaviorTree.SetVariableValue("CurrentState", EnemyAIStates.SpecialAbility2);
        }
        
        
        
    }
}