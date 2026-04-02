using System;
using System.Collections.Generic;
using Entity.Enemy.WarriorEnemy;
using UnityEngine;

namespace Entity.Enemy
{
    public class EnemyAttackQueue
    {
        public int maxConcurrentAttacks = 1;
        private int currentAttackingCount = 0;
        
        
        private LinkedList<EnemyAttackController> attackQueue = new LinkedList<EnemyAttackController>();
        
        private Dictionary<EnemyAttackController, LinkedListNode<EnemyAttackController>> nodeMap = new();


        public void RequestAttack(EnemyAttackController enemy)
        {
            if (!attackQueue.Contains(enemy))
            {
                nodeMap[enemy] = attackQueue.AddLast(enemy);
            }
        }
        
        public bool CanAttack(EnemyAttackController enemy)
        {
            if (currentAttackingCount < maxConcurrentAttacks && attackQueue.Last.Value == enemy)
            {
                attackQueue.RemoveLast();
                nodeMap.Remove(enemy);
                currentAttackingCount++;
                return true;
            }
            return false;
        }
        
        public void CancelAttack(EnemyAttackController enemy)
        {
            if (nodeMap.TryGetValue(enemy, out var node))
            {
                attackQueue.Remove(node);
                nodeMap.Remove(enemy);
                currentAttackingCount = Mathf.Max(0, currentAttackingCount - 1);
            }
        }
        
        public void FinishAttack()
        {
            currentAttackingCount = Mathf.Max(0, currentAttackingCount - 1);
        }
        
    }
}