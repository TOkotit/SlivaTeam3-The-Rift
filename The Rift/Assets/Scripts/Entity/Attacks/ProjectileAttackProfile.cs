using UnityEngine;

namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Attacks/Projectile attack")]
    public class ProjectileAttackProfile : AttackProfile
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private float power;
        public GameObject Projectile => projectile; 
        public float Power => power;
    }
}