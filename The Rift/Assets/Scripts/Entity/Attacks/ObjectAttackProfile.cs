using UnityEngine;

namespace Entity.Attacks
{
    [CreateAssetMenu(fileName = "ObjectAttack", menuName = "Attacks/Object attack")]
    public class ObjectAttackProfile : AttackProfile
    {
        [SerializeField] private GameObject _object;
        public GameObject Object => _object; 
    }
}