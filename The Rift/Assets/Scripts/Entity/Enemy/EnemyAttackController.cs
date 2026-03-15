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
    [SerializeField] private RaycastAttackProfile _attackProfile;
    private Weapon _weaponProfile;
    [Inject] private WeaponManager _weaponManager;
    [Inject] private AttackSystem _attackSystem;
    [Inject] private EnemyAttackQueue _attackQueue;
    


    public EnemyAttackQueue AttackQueue
    {
        get => _attackQueue;
        set => _attackQueue = value;
    }

    private void Start()
    {
        _weaponProfile = _weaponManager.CreateWeapon("TestWeapon");
    }

    public void Attack()
    {

        _attackSystem.PerformAttack(_attackProfile, _weaponProfile, gameObject, Teams.Enemy);
    }
    
}
