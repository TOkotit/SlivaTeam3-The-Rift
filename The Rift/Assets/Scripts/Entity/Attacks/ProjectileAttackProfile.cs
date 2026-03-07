using UnityEngine;

namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Attacks/Projectile attack")]
    public class ProjectileAttackProfile : AttackProfile
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private float power;
        public Projectile Projectile => projectile; 
        public float Power => power;
    }
}