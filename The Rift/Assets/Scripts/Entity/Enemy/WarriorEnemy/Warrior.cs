using System;
using MainCharacter;
using TMPro;
using Unity.Behavior;
using UnityEngine;
using VContainer;


namespace Entity.Enemy.WarriorEnemy
{
    public class Warrior : Enemy
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI parryText;
        
        [SerializeField] private GameObject attackChargeIndicator;
        [SerializeField] private GameObject parryIndicator;
        
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private EnemyAttackController _attackController;
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private Animator _animator;
        [Inject] MainCharacterAttackController mainCharacterAttackController;
        
        private BehaviorGraphAgent behaviorTree;
        private int _hitCounter = 0;
        private IAttackProfile _lastHittedAttack;
        
        private float _attackPauseTimer;
        private bool _attackPauseTimerStarted;
        
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
        
        public void UpdateParryText()
        {
            parryText.text = $"{_hitCounter}";
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
       public void InitializeBlackboard()
        {
            behaviorTree.SetVariableValue("CurrentState", WarriorAiStates.Idle);
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
            
            
            _attackChargeIndicator = attackChargeIndicator;
            _parryIndicator = parryIndicator;
            
            behaviorTree = GetComponent<BehaviorGraphAgent>();
            InitializeBlackboard();
            _attackController.EnemyModel = _enemyModel;
            
            
            UpdateHealthText(Damagable.Health.CurrentHealth);
            
            Damagable.Health.OnHealthChanged += UpdateHealthText;


            Damagable.OnTakeHit += IncHitCounter;
            Damagable.OnTakeHit += UpdateParryText;
            Damagable.OnTakeHit += StartAttackPauseTimer;
            Damagable.Health.OnDeath += Die;
            
            Damagable.OnTakeHit += OnGotHitAnimation;
        }

        public void Update()
        {
            if (_attackPauseTimerStarted)
            {
                _attackPauseTimer += Time.deltaTime;
                if (_attackPauseTimer >= 2.5f)
                {
                    DashBack();
                    _attackPauseTimer = 0f;
                    _attackPauseTimerStarted = false;
                }
            }
        }
        
        public new void OnDestroy()
        {
            Damagable.Health.OnDeath -= Die;
            
            Damagable.Health.OnHealthChanged -= UpdateHealthText;
            
            Damagable.OnTakeHit -= IncHitCounter;
            Damagable.OnTakeHit -= UpdateParryText;
            Damagable.OnTakeHit -= StartAttackPauseTimer;
            
            Damagable.OnTakeHit -= OnGotHitAnimation;
            
            base.OnDestroy();
        }
        
        private void Die()
        {
            _attackPauseTimerStarted = false;
            behaviorTree.SetVariableValue("CurrentState", WarriorAiStates.Dead);
        }
        
        public void DashBack()
        {
            if (_targetDetector.IsTargetVisible )
            {
                behaviorTree.SetVariableValue("CurrentState", WarriorAiStates.SpecialAbility1);
            }
        }

        public void StartAttackPauseTimer()
        {
            _attackPauseTimer = 0;
            _attackPauseTimerStarted  = true;
        }
        
        public void Block()
        {
            if (_targetDetector.IsTargetVisible)
            {
                behaviorTree.SetVariableValue("CurrentState", WarriorAiStates.SpecialAbility2);
            }
        }
        
        private void IncHitCounter()
        {
            
            if (_lastHittedAttack == mainCharacterAttackController.LastAttack)
            {
                _hitCounter++;
                if (_hitCounter >= 2)
                {
                    _hitCounter = 0;
                    Block();
                    _lastHittedAttack = null;
                }
                else
                {
                    _lastHittedAttack = mainCharacterAttackController.LastAttack;
                }
            }
            else
            {
                _lastHittedAttack = mainCharacterAttackController.LastAttack;
            }
        }

        private void OnGotHitAnimation()
        {
            _animator.SetTrigger("GotHit");
        }

    }
}