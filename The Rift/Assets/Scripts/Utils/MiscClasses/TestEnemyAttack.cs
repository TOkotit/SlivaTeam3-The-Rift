using System;
using System.Collections;
using Entity.Enemy;
using Systems;
using UnityEngine;
using VContainer;

namespace Utils.MiscClasses
{
    public class TestEnemyAttack : Enemy
    {
        private bool canAttack =  true;
        [SerializeField]
        private CloseEnemyAttackData _enemyAttackData;
        [Inject]
        private AttackSystem _attackSystem;
        [Inject]
        private ICoroutineRunner _coroutineRunner;

        private void Update()
        {
            if (canAttack)
            {
                _coroutineRunner.StartRoutine(attackCoroutine());
            }
        }

        private IEnumerator attackCoroutine()
        {
            canAttack = false;
            Debug.Log("Attacking..." + this.gameObject + " " + _enemyModel+ " " + _enemyAttackData);
            _attackSystem.PerformEnemyAttack(_enemyAttackData, _enemyModel, this.gameObject);
            yield return new WaitForSeconds(4f);
            canAttack = true;
        }
    }
}