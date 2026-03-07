using System;
using System.Collections.Generic;
using UnityEngine;


namespace Entity.Enemy
{
    public class EnemyArea : MonoBehaviour
    {
        [SerializeField] List<Warrior.Warrior> warriors;
        private int currentWarriorIndex = 0;

        private void Start()
        {
            foreach (var warrior in warriors)
            {
                warrior.AttackController.HasAttacked += b => OnAnyWarriorAttacked();
            }
            
            SetCurrentAttack(true);
        }
        
        
        private void OnAnyWarriorAttacked()
        {
            SetCurrentAttack(false);
            NextWarrior();
            SetCurrentAttack(true);
        }
        
        public void NextWarrior()
        {
            currentWarriorIndex++;
            if (currentWarriorIndex >= warriors.Count) currentWarriorIndex = 0;
        }
        public void SetCurrentAttack(bool isAbleToAttack)
        {
            if (warriors[currentWarriorIndex] == null)
            {
                NextWarrior();
                return;
            }
            warriors[currentWarriorIndex].AttackController.IsAbleToAttack = isAbleToAttack;
        }
    }
}