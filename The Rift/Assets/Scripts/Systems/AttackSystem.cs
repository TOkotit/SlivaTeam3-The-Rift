using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Attacks;
using Enums;
using MainCharacter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;
using VContainer;

namespace Systems
{
    public class AttackSystem
    {
        static AttackSystem instance;
        
        [Inject]
        private readonly DamagableRegistry _registry;
        [Inject]
        private ICoroutineRunner _coroutineRunner;
        
        public AttackSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AttackSystem();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        
        public void PerformAttack(IAttackProfile profile, Weapon weaponProfile,  GameObject sender, Teams team)
        {
            
            Debug.Log("attack was performed");
            if (profile is RaycastAttackProfile raycastProfile)
            {
                Debug.Log("Starting coroutine");
                _coroutineRunner.StartRoutine(
                    CastRaysContinuous(raycastProfile, weaponProfile, sender, team)
                );
            }
            
        }
        

        private IEnumerator CastRaysContinuous(RaycastAttackProfile attackProfile, Weapon weaponProfile, GameObject sender, Teams team) 
        {
            var range = weaponProfile.Model.Range * attackProfile.DistanceMultiplier;
            var damage = weaponProfile.Model.Damage * attackProfile.DamageMultiplier;
            var piercing = weaponProfile.Model.Piercing;
            var swingSpeed = weaponProfile.Model.SwingSpeed; 
            var totalAngle = attackProfile.Angle;            
            var tilt = attackProfile.Tilt;                    
            var halfAngle = totalAngle * 0.5f;
            var waitTime = (swingSpeed > 0 ? 1f / swingSpeed : 0f) / totalAngle;

            Debug.Log("CastRaysContinuous started");
            for (float angle = -halfAngle; angle <= halfAngle; angle += 1f)
            {
                var baseDirection = sender.transform.forward;
                var horizontalRotation = Quaternion.AngleAxis(angle, Vector3.up);
                var directionAfterYaw = horizontalRotation * baseDirection;
                var tiltRotation = Quaternion.AngleAxis(tilt, sender.transform.forward);
                var finalDirection = tiltRotation * directionAfterYaw;
                var ray = new Ray(sender.transform.position, finalDirection);
                var hits = Physics.RaycastAll(ray, range);
                
                Vector3 endPoint = ray.origin + ray.direction * range;
                GameObject lineObj = new GameObject("AttackRay");
                LineRenderer lr = lineObj.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, endPoint);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.material = new Material(Shader.Find("Sprites/Default")); // или ваш материал
                lr.startColor = Color.red;
                lr.endColor = Color.red; 
                CoroutineRunner.Destroy(lineObj, 0.1f);
                
                
                foreach (RaycastHit hit in hits)
                {
                    var targetModel = _registry.TryGetCharacter(hit.collider);
                    if (targetModel == null) continue;
                    if (targetModel.Team == team) continue;

                    targetModel.Health.TakeDamage(Mathf.RoundToInt(damage), attackProfile.DamageType);

                    if (!piercing)
                        yield break; 
                        
                }


                if (waitTime > 0f)
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
            
            Debug.Log("CastRaysContinuous finished" );
        }
        
    }
}