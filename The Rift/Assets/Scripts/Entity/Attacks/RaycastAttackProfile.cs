using Entity;
using Enums;
using UnityEngine;

namespace MainCharacter
{
    public class RaycastAttackProfile : ScriptableObject, IAttackProfile 
    {
        [SerializeField] private float distance;
        [SerializeField] private float angle;
        [SerializeField] private float tilt;
        [SerializeField] private DamageTypes  damageType;
        private string _name;
        public DamageTypes DamageType => damageType;
        public float Distance => distance;
        public float Angle => angle;
        public float Tilt => tilt;
        public float Damage {get; set;}

        string IAttackProfile.Name
        {
            get => _name;
            set => _name = value;
        }
    }
}