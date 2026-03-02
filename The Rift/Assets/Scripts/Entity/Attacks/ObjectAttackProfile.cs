using UnityEngine;

namespace Entity.Attacks
{
    public class ObjectAttackProfile : AttackProfile
    {
        [SerializeField] private GameObject _object;
        public GameObject Object => _object;
    }
}