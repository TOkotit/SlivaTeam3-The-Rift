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
    [SerializeField] private Weapon _weaponProfile;
    [Inject] private AttackSystem _attackSystem;
    [Inject] private EnemyAttackQueue _attackQueue;
    


    public EnemyAttackQueue AttackQueue
    {
        get => _attackQueue;
        set => _attackQueue = value;
    }


    public void Attack()
    {
        StartCoroutine(RotateRoutine( 1));
        
        // _attackSystem.PerformAttack(_attackProfile, _weaponProfile, gameObject, Teams.Enemy);
        
    }
    
    private IEnumerator RotateRoutine(float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            // Вычисляем фактор t от 0 до 1
            float t = elapsed / duration;
            
            // Используем Slerp для плавной интерполяции
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            
            yield return null; // Ждем следующего кадра
        }

        // Финальная установка, чтобы точно попасть в таргет
        transform.rotation = targetRotation;
    }
}
