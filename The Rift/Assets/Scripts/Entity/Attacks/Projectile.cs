using Enums;
using UnityEngine;
using VContainer;

namespace Entity.Attacks
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [Inject]
        private static DamagableRegistry _damagableRegistry;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private bool _fragile;
        private int _damage;
        private DamageTypes _damageType;
        public Projectile(int damage, bool fragile, DamageTypes damageType)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _damage = damage;
            _fragile = fragile;
            _damageType = damageType;
        }

        public void Launch(Vector3 direction, float velocity)
        {
            _rigidbody.AddForce(direction * velocity, ForceMode.Impulse);
        }
        
        void OnCollisionEnter(Collision collision)
        {
            var surface =  collision.gameObject;
            var damagable = _damagableRegistry.TryGetCharacter(surface);
            damagable.Health.TakeDamage(_damage, _damageType);
            if (_fragile)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.SetParent(collision.gameObject.transform);
                Destroy(_rigidbody);
                _rigidbody = null;
            }
        }
    }
}