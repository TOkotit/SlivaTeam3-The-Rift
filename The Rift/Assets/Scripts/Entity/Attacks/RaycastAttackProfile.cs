using Entity;
using Entity.Attacks;
using Enums;
using UnityEngine;

namespace MainCharacter
{
    [CreateAssetMenu(fileName = "RaycastAttack", menuName = "Attacks/Raycast attacks")]
    public class RaycastAttackProfile : AttackProfile
    {
        [SerializeField] private float angle;
        [SerializeField] private float tilt;
        [SerializeField] private float damageMultiplier;
        [SerializeField] private float distanceMultiplier;
        [SerializeField] private bool equalizeDamage;
        public float Angle => angle;
        public float Tilt => tilt;
        public float DamageMultiplier => equalizeDamage? damageMultiplier * angle / 3 : damageMultiplier;
        public float DistanceMultiplier => distanceMultiplier;
    }
}