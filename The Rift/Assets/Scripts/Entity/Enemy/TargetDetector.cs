using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Entity.Enemy
{
    public class TargetDetector : MonoBehaviour
    { 
        [Header("Proximity Settings")]
        [SerializeField] private Transform _proximityAreaCenter;
        [SerializeField] private float _proximityAreaRadius;

        [Header("Sight Settings")]
        [SerializeField] private float _sightDistance;
        [SerializeField] private float _sightAngle; 
        [SerializeField] private LayerMask _targetLayer;   
        [SerializeField] private LayerMask _enemyLayer;  
        [SerializeField] private LayerMask _obstacleLayer; 
        
        [Header("Sharing Signal Settings")]
        [SerializeField] private Transform _sharingAreaCenter;
        [SerializeField] private float _sharingAreaRadius;
        public Transform DetectedTarget { get; private set; }
        public float DistanceToTarget { get; private set; } = -1f;
        public bool IsTargetVisible { get; private set; }
        
        public bool IsTargetVisibleByGroup { get; private set; }
        
        public void SetTargetFromOther(Transform detectedTarget, float distanceToTarget, bool isTargetVisible)
        {
            Debug.Log("SetTargetFromOther");
            DetectedTarget = detectedTarget;
            DistanceToTarget = distanceToTarget;
            IsTargetVisibleByGroup = isTargetVisible;
            IsTargetVisible = isTargetVisible;
        }

        public float UpdateDistanceToTarget()
        {
            return Vector3.Distance(transform.position, DetectedTarget.position);
        }
        
        public void DetectPlayer()
        {
            var maxRange = Mathf.Max(_proximityAreaRadius, _sightDistance);
            var targetsInRadius = new Collider[10];
            var size = Physics.OverlapSphereNonAlloc(transform.position, maxRange, targetsInRadius, _targetLayer);
            
            if (size > 0)
            {
                var target = targetsInRadius[0].transform;
                var directionToTarget = (target.position - transform.position).normalized;
                var distance = Vector3.Distance(transform.position, target.position);
                
                var inProximity = distance <= _proximityAreaRadius;
                
                var inSight = false;
                if (distance <= _sightDistance)
                {
                    var angle = Vector3.Angle(transform.forward, directionToTarget);
                    if (angle < _sightAngle / 2f)
                    {
                        if (!Physics.Raycast(transform.position, directionToTarget, distance, _obstacleLayer))
                        {
                            inSight = true;
                        }
                    }
                }

                if (inProximity || inSight)
                {
                    DetectedTarget = target;
                    DistanceToTarget = distance;
                    IsTargetVisible = true;
                    
                    if (!IsTargetVisibleByGroup)
                    {
                        var enemiesInRadius = new Collider[10];
                        var enemiesSize = Physics.OverlapSphereNonAlloc(transform.position, 
                            _sharingAreaRadius, 
                            enemiesInRadius, 
                            _enemyLayer);
                        
                        if (enemiesSize > 0)
                        {
                            Debug.Log("FOR ENEMIES: TARGET DETECTED IN AREA");
                            foreach (var enemy in enemiesInRadius)
                            {
                                enemy?.GetComponent<TargetDetector>()?.SetTargetFromOther(DetectedTarget,
                                    DistanceToTarget, IsTargetVisible);
                            }
                        }
                    }
                    return;
                }
            }
            
            DetectedTarget = null;
            DistanceToTarget = -1f;
            IsTargetVisible = false;
            IsTargetVisibleByGroup = false;
        }
        
        
        
        // Отрисовка зон в редакторе
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_proximityAreaCenter ? _proximityAreaCenter.position : transform.position, _proximityAreaRadius);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_sharingAreaCenter ? _sharingAreaCenter.position : transform.position, _sharingAreaRadius);
            
            Gizmos.color = Color.red;
            var leftRayRotation = Quaternion.AngleAxis(-_sightAngle / 2, Vector3.up);
            var rightRayRotation = Quaternion.AngleAxis(_sightAngle / 2, Vector3.up);
            var leftRayDirection = leftRayRotation * transform.forward;
            var rightRayDirection = rightRayRotation * transform.forward;

            Gizmos.DrawRay(transform.position, leftRayDirection * _sightDistance);
            Gizmos.DrawRay(transform.position, rightRayDirection * _sightDistance);
        }
    }
}