using System;
using Enums;
using UnityEngine;
using VContainer;

namespace Entity.Attacks
{
    public class Projectile : MonoBehaviour
    {
        [Inject]
        private DamagableRegistry _damagableRegistry;

        [SerializeField] private Vector3 _correctionAngles = new Vector3(-90, 0, 0);
        private Quaternion _correction;

        private Rigidbody _rigidbody;
        private Collider _collider;
        private bool _fragile;
        private int _damage;
        private DamageTypes _damageType;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _correction = Quaternion.Euler(_correctionAngles);
        }

        public void Initialize(int damage, bool fragile, DamageTypes damageType)
        {
            _damage = damage;
            _fragile = fragile;
            _damageType = damageType;
        }

        public void Launch(Vector3 direction, float velocity)
        {
            if (!_rigidbody) return;
            _rigidbody.linearVelocity = direction.normalized * velocity;
            UpdateRotation();
        }

        private void FixedUpdate()
        {
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            if (!_rigidbody) return;

            Vector3 velocity = _rigidbody.linearVelocity;
            if (velocity.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized) * _correction;
                transform.rotation = targetRotation;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("OnCollisionEnter: " + collision.gameObject);

            var damagable = _damagableRegistry?.TryGetCharacter(collision.gameObject);
            damagable?.Health.TakeDamage(_damage, _damageType);

            if (_fragile)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.SetParent(collision.gameObject.transform);
                if (_rigidbody) Destroy(_rigidbody);
                if (_collider) Destroy(_collider);
                _rigidbody = null;
                _collider = null;
                StartCoroutine(DestroyAfterDelay(5f));
            }
        }

        private System.Collections.IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}