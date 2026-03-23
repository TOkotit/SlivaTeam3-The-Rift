using Enums;
using MainCharacter;
using UnityEngine;
using VContainer;

namespace Entity.Enemy
{
    public class Enemy : Character
    {
        [Inject] protected EnemyModel _enemyModel;
        public override DamagableModel Damagable => _enemyModel;
        public EnemyModel EnemyModel => _enemyModel; 
        
        protected GameObject _parryArea;
        protected GameObject _attackChargeIndicator;
        protected GameObject _parryIndicator;
        public GameObject ParryArea => _parryArea;
        public GameObject AttackChargeIndicator => _attackChargeIndicator;
        public GameObject ParryIndicator => _parryIndicator;
        
    }
}