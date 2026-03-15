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
        
        
        
        
    }
}