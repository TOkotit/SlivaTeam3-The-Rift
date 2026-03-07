using System;
using Entity;
using Entity.Attacks;
using Enums;
using MainCharacter;
using Systems;
using UnityEngine;
using VContainer;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private RaycastAttackProfile _attackProfile;
    [SerializeField] private Weapon _weaponProfile;
    [Inject] private AttackSystem _attackSystem;
    
    public Action<bool> HasAttacked;
    public bool IsAbleToAttack;
    

    public void Attack()
    {
        // _attackSystem.PerformAttack(_attackProfile, _weaponProfile, gameObject, Teams.Enemy);
        HasAttacked?.Invoke(true);
    }
}
