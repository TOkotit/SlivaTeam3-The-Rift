using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory.Runes.Runes_Data
{
    public class ShaetAtractor : MonoBehaviour
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