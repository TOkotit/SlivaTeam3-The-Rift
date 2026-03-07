using System;
using System.Collections.Generic;
using Entity.Enemy.WarriorEnemy;
using UnityEngine;

namespace Entity.Enemy
{
    public class EnemyAttackQueue
    {
        // Очередь врагов, ожидающих атаки
        private Queue<EnemyAttackController> attackQueue = new Queue<EnemyAttackController>();
    
        // Максимальное количество одновременно атакующих (обычно 1 или 2)
        public int maxConcurrentAttacks = 1;
        private int currentAttackingCount = 0;

        // Враг встает в очередь
        public void RequestAttack(EnemyAttackController enemy)
        {
            if (!attackQueue.Contains(enemy))
            {
                attackQueue.Enqueue(enemy);
            }
        }

        // Проверка: может ли этот враг атаковать сейчас?
        public bool CanAttack(EnemyAttackController enemy)
        {
            // Если лимит не исчерпан и враг первый в очереди
            if (currentAttackingCount < maxConcurrentAttacks && attackQueue.Peek() == enemy)
            {
                attackQueue.Dequeue();
                currentAttackingCount++;
                return true;
            }
            return false;
        }

        // Враг сообщает, что закончил атаку
        public void FinishAttack()
        {
            currentAttackingCount = Mathf.Max(0, currentAttackingCount - 1);
        }
    }
}