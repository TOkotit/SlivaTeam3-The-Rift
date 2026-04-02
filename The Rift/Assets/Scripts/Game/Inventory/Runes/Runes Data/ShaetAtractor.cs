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
            var stopDistance = 0.5f;
            var elapsed = 0f;
            var results = new Collider[20];
            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (elapsed < duration)
            {
                var count = Physics.OverlapSphereNonAlloc(center, radius, results, targetLayer);
        
                for (var i = 0; i < count; i++)
                {
                    if (results[i].TryGetComponent<NavMeshAgent>(out var agent))
                    {
                        if (!agent.enabled) continue;

                        var directionToCenter = (center - agent.transform.position);
                        directionToCenter.y = 0;
                
                        var distance = directionToCenter.magnitude;
                        
                        if (distance > stopDistance) 
                        {
                            var pullVelocity = directionToCenter.normalized * force;
                            agent.velocity += pullVelocity * Time.fixedDeltaTime;
                        }
                        else 
                        {
                            agent.velocity = Vector3.Lerp(agent.velocity, Vector3.zero, Time.fixedDeltaTime * 5f);
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