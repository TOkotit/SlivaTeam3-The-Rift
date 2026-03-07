using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity.Enemy.Warrior
{
    public class Warrior : Enemy
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private EnemyAttackController _attackController;
        public Action<Warrior> EnemyTriggered;
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

        public new void Start()
        {
            base.Start();
            
        }
    }
}