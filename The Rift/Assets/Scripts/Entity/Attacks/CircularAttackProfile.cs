using Entity;
using Enums;
using UnityEngine;

    

namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "CircularAttack", menuName = "Attacks/Circular attack")]
    public class CircularAttackProfile  : AttackProfile
    {
        [SerializeField] private float tilt;
        [SerializeField] private float radiusMultiplyer;
        public float Tilt => tilt;
        public float RadiusMultiplyer => radiusMultiplyer;
    }
}
