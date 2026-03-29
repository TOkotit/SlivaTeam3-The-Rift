

using Enums;
using UnityEngine;

namespace Entity.Enemy
{
    public class EnemyAttackData : ScriptableObject
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private int _damage;
        [SerializeField] private DamageTypes _damageType;
        public Vector3 Offset => _offset;
        public int Damage => _damage;
        public DamageTypes  DamageType => _damageType;
    }
}