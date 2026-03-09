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
        
        public int maxConcurrentAttacks = 1;
        private int currentAttackingCount = 0;
        
        public void RequestAttack(EnemyAttackController enemy)
        {
            if (!attackQueue.Contains(enemy))
            {
                attackQueue.Enqueue(enemy);
            }
        }
        
        public bool CanAttack(EnemyAttackController enemy)
        {
            if (currentAttackingCount < maxConcurrentAttacks && attackQueue.Peek() == enemy)
            {
                attackQueue.Dequeue();
                currentAttackingCount++;
                return true;
            }
            return false;
        }
        
        public void FinishAttack()
        {
            currentAttackingCount = Mathf.Max(0, currentAttackingCount - 1);
        }
    }
}