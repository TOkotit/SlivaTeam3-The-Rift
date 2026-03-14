using Entity;
using Enums;
using UnityEngine;

    

namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "CircularAttack", menuName = "Attacks/Circular attack")]
    public class CircularAttackProfile  : AttackProfile
    {
        [SerializeField] private float tilt;
        [SerializeField] private Vector2 metrics;
        [SerializeField] private bool piercing;
        public float Tilt => tilt;
        public Vector2 Metrics => metrics;
        
        public bool Piercing => piercing;
    }
}
