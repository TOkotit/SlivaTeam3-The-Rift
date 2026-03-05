using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity.Enemy.Warrior
{
    public class Warrior : Enemy
    {

        [SerializeField] private TargetDetector _targetDetector;
        

        public new void Start()
        {
            base.Start();
            
        }
    }
}