using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory.Runes.Runes_Data
{
    public class KinematicPuller : MonoBehaviour
    {
        [Header("Settings")]
        private List<Transform> affectedTransforms = new ();
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
            var visualSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visualSphere.transform.position = center;
            visualSphere.transform.localScale = Vector3.one * radius * 2f;
            Destroy(visualSphere.GetComponent<Collider>());
            
            var renderer = visualSphere.GetComponent<Renderer>();
            renderer.material.color = new Color(0, 1, 0, 0.1f);
            
            visualSphere.transform.SetParent(this.transform);
            
            
            StartCoroutine(PullRoutine());

        }

        IEnumerator PullRoutine()
        {
            float elapsed = 0;
            
            var results = new Collider[20];
            
            while (elapsed < duration)
            {
                
                var count = Physics.OverlapSphereNonAlloc(center, radius, results, targetLayer);      
                
                
                for (var i = 0; i < count; i++)
                {
                    var target = results[i].transform;
                
                    if (target == null) continue;

                    var targetPos = new Vector3(center.x, target.position.y, center.z);

                    target.position = Vector3.MoveTowards(
                        target.position, 
                        targetPos, 
                        force * Time.deltaTime
                    );
                }
                elapsed += Time.deltaTime;
                yield return null; 
            }
            onDestroyCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}