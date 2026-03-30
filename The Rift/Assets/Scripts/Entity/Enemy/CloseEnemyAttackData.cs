using UnityEngine;

namespace Entity.Enemy
{
    [CreateAssetMenu(fileName = "CloseEnemyAttackData", menuName = "MainCharacter/CloseEnemyAttackData")]
    public class CloseEnemyAttackData : EnemyAttackData
    {
        [SerializeField] private Vector3 _metrics;
        [SerializeField] private float _parryDuration;
        public Vector3 Metrics =>  _metrics;
        public float ParryDuration => _parryDuration;
        
    }
}