using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Entity.Attacks;
using Entity.Enemy;
using Enums;
using MainCharacter;
using Systems;
using UnityEngine;
using VContainer;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private EnemyAttackData _attackData;
    
    // [Inject] private WeaponManager _weaponManager;
    [Inject] private AttackSystem _attackSystem;
    [Inject] private EnemyAttackQueue _attackQueue;
    
    private EnemyModel _enemyModel;
    

    public EnemyAttackQueue AttackQueue
    {
        get => _attackQueue;
        set => _attackQueue = value;
    }

    public EnemyModel EnemyModel
    {
        get => _enemyModel;
        set => _enemyModel = value;
    }

    public void Attack()
    {
       _attackSystem.PerformEnemyAttack(_attackData, _enemyModel, gameObject);
    }
    
}
