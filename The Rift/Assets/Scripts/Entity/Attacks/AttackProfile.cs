using System.Collections.Generic;
using AYellowpaper;
using Enums;
using UnityEngine;

namespace Entity.Attacks
{
    public class AttackProfile : ScriptableObject, IAttackProfile
    {
        
        [SerializeField] private DamageTypes  damageType;
        [SerializeField] private float damageMultiplier; 
        [SerializeField] private string _name;
        [SerializeField] private float cooldown;
        [SerializeField] private Vector3 offset;
        [SerializeField] private bool randomizeOffsetInBounds;
        [SerializeField] private Vector2 angularOffset;
        [SerializeField] private bool randomizeAngularOffset;
        [SerializeField] private List<InterfaceReference<IAttackEvent>> events;
        
        public float DamageMultiplier => damageMultiplier;
        public DamageTypes DamageType => damageType;
        public string Name => _name;
        public float Cooldown => cooldown;
        public List<InterfaceReference<IAttackEvent>> Events => events;

        public Vector2 AngularOffset
        {
            get
            {
                if (!randomizeAngularOffset) return angularOffset;
                return new Vector2(Random.value * angularOffset.x, Random.value * angularOffset.y);
            }

        }
        public bool RandomizeAngularOffset => randomizeAngularOffset;
        public Vector3 Offset
        {
            get
            {
                if (!randomizeOffsetInBounds) return offset;
                return new Vector3(Random.value * offset.x, Random.value * offset.y, Random.value * offset.z);
            } 
        }
        public bool RandomizeOffsetInBounds => randomizeOffsetInBounds;
    }
}