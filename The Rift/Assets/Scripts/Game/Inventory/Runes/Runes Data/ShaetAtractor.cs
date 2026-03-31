using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Inventory.Runes.Runes_Data
{
    public class ShaetAtractor : MonoBehaviour
    {
        [Header("Settings")]
        private List<EnemyMovementController> _affectedControllers = new();
        private Vector3 center;
        private float force;
        private float duration;
        private float radius;
        private LayerMask targetLayer;
        private Action onDestroyCallback;
        
        public void Initialize(Vector3 centerPoint, float pullForce, float time, float range, LayerMask mask, Action onFinished)
        {
            center = centerPoint;
            force = pullForce;
            duration = time;
            radius = range;
            targetLayer = mask;
            onDestroyCallback = onFinished;
            
            
            
            // рендер для дебага
            var visualDisk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            visualDisk.transform.position = center + Vector3.up * 0.05f; 

            visualDisk.transform.localScale = new Vector3(radius * 2f, 0.01f, radius * 2f);

            Destroy(visualDisk.GetComponent<Collider>());

            var renderer = visualDisk.GetComponent<Renderer>();
            
            renderer.material = new Material(Shader.Find("Sprites/Default")); 
            renderer.material.color = new Color(0, 1, 0, 0.2f); 

            visualDisk.transform.SetParent(this.transform);
            
            
            StartCoroutine(PullRoutine());

        }

        IEnumerator PullRoutine()
        {
            float elapsed = 0;
            var results = new Collider[20];
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (elapsed < duration)
            {
                var count = Physics.OverlapSphereNonAlloc(center, radius, results, targetLayer);
        
                for (var i = 0; i < count; i++)
                {
                    // Пытаемся достать агента напрямую
                    if (results[i].TryGetComponent<NavMeshAgent>(out var agent))
                    {
                        if (!agent.enabled) continue;

                        // Вектор к центру притягивания
                        Vector3 directionToCenter = (center - agent.transform.position);
                        directionToCenter.y = 0; // Игнорируем высоту
                
                        float distance = directionToCenter.magnitude;
                
                        if (distance > 0.1f)
                        {
                            // Нормализуем и умножаем на силу
                            Vector3 pullVelocity = directionToCenter.normalized * force;

                            // Плавное подмешивание силы притягивания к текущей скорости агента
                            // Это позволит ему "пытаться" идти в свою сторону, но его будет тянуть
                            agent.velocity += pullVelocity * Time.fixedDeltaTime;
                        }
                    }
                }

                elapsed += Time.fixedDeltaTime;
                yield return waitForFixedUpdate;
            }

            onDestroyCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}