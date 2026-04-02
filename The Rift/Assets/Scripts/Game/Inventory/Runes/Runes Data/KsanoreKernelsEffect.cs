using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Inventory.Runes.Runes_Data
{
    public class KsanoreKernelsEffect : MonoBehaviour
    {
        private Vector3 _localOffset;
        private bool _isAttached;
        private float _duration;
        private Action _onDestroyCallback;
        private List<GameObject> _spawnedKernels = new();
        
        
        private LayerMask _enemyLayer;
        private Collider[] _results = new Collider[20];
        private float _damage;
        private float _checkRadius = 1.2f;
    
        public void Initialize(Vector3 hitPoint, GameObject prefab, int count, float duration, bool hitEnemy, float damage, LayerMask layerMask, Transform parent, Action onFinished)
        {
            _duration = duration;
            _onDestroyCallback = onFinished;
            _enemyLayer = layerMask;
            _damage = damage;
            _isAttached = hitEnemy && parent != null;

            if (_isAttached)
            {
                transform.SetParent(parent);
                _localOffset = parent.InverseTransformPoint(hitPoint);
                transform.localPosition = _localOffset;
            }
            else
            {
                transform.position = hitPoint;
            }

            
            SpawnKernels(prefab, count, hitEnemy, Vector3.zero);

            ApplySteelDamage();

            StartCoroutine(EffectRoutine());
        }

        private void SpawnKernels(GameObject prefab, int count, bool hitEnemy, Vector3 localCenter)
        {
            for (var i = 0; i < count; i++)
            {
                var spawnPos = localCenter;
                Quaternion rotation;

                if (hitEnemy)
                {
                    rotation = Random.rotation;
                }
                else
                {
                    var radius = 2f;
                    var angle = i * Mathf.PI * 2f / count;
                    var offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                    spawnPos = localCenter + offset;
                    var lookDir = (localCenter - spawnPos).normalized + Vector3.up * 0.5f;
                    rotation = Quaternion.LookRotation(lookDir);
                }

                var k = Instantiate(prefab, transform); 
                k.transform.localPosition = spawnPos;
                k.transform.localRotation = rotation;
                
                _spawnedKernels.Add(k);
            }
        }
        
        
        private void ApplySteelDamage()
        {
            var hitModels = new HashSet<DamagableModel>();

            foreach (var kernel in _spawnedKernels)
            {
                if (kernel == null) continue;

                var count = Physics.OverlapSphereNonAlloc(kernel.transform.position, _checkRadius, _results, _enemyLayer);
                
                
                for (var i = 0; i < count; i++)
                {
                    var col = _results[i];
                    
                    var character = col.GetComponentInParent<Entity.Enemy.Enemy>();
                    
                    if (character != null)
                    {
                        var targetModel = character.Damagable;

                        if (targetModel != null && !hitModels.Contains(targetModel))
                        {
                            targetModel.Health.TakeDamage(_damage, DamageTypes.Slice);
                            targetModel.TakeHit();
                            
                            hitModels.Add(targetModel);
                            Debug.Log($"<color=silver>Ксанор: удар по {col.name}</color>");
                        }
                    }
                }
            }
        }

        IEnumerator EffectRoutine()
        {
            var elapsed = 0f;
            var tickTimer = 0f;

            while (elapsed < _duration)
            {
                elapsed += Time.deltaTime;
                tickTimer += Time.deltaTime;

                if (tickTimer >= 1f)
                {
                    ApplySteelDamage();
                    tickTimer -= 1f;
                }

                yield return null;
            }

            _onDestroyCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}