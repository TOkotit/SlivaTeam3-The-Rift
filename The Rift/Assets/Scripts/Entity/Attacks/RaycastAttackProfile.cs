using Entity;
using Enums;
using UnityEngine;

namespace MainCharacter
{
    [CreateAssetMenu(fileName = "RaycastAttack", menuName = "Attacks/Raycast attacks")]
    public class RaycastAttackProfile : ScriptableObject, IAttackProfile 
    {
        [SerializeField] private float angle;
        [SerializeField] private float tilt;
        [SerializeField] private DamageTypes  damageType;
        [SerializeField] private float damageMultiplier;
        [SerializeField] private float distanceMultiplier;
        private string _name;
        public DamageTypes DamageType => damageType;
        public float Angle => angle;
        public float Tilt => tilt;
        public float DamageMultiplier => damageMultiplier;
        public float DistanceMultiplier => distanceMultiplier;

        string IAttackProfile.Name
        {
            get => _name;
            set => _name = value;
        }
    }
}