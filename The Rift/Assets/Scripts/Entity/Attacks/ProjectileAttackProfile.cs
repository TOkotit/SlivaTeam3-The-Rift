using UnityEngine;

namespace Entity.Attacks
{
    public class ProjectileAttackProfile : AttackProfile
    {
        [SerializeField] private GameObject projectile;
        public GameObject Projectile => projectile;
        [SerializeField] private float power;
        public float Power => power;
    }
}