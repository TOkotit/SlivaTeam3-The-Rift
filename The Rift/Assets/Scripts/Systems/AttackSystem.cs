using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Enums;
using MainCharacter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Systems
{
    public class AttackSystem
    {
        static AttackSystem instance;
        
        [Inject]
        private readonly DamagableRegistry _registry;
        [Inject]
        private CoroutineRunner _coroutineRunner;
        
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
        
        public void PerformAttack(IAttackProfile profile, WeaponProfile weaponProfile,  GameObject sender, Teams team)
        {
            if (profile is RaycastAttackProfile raycastProfile)
            {
                _coroutineRunner.StartCoroutine(
                    CastRaysContinuous(raycastProfile, weaponProfile, sender, team)
                );
            }
            
        }
        

        private IEnumerator CastRaysContinuous(RaycastAttackProfile attackProfile, WeaponProfile weaponProfile, GameObject sender, Teams team) 
        {
            var range = weaponProfile.Range * attackProfile.DistanceMultiplier;
            var damage = weaponProfile.Damage * attackProfile.DamageMultiplier;
            var piercing = weaponProfile.Piercing;
            var swingSpeed = weaponProfile.SwingSpeed; 
            var totalAngle = attackProfile.Angle;            
            var tilt = attackProfile.Tilt;                    
            var baseDirection = sender.transform.forward;
            var halfAngle = totalAngle * 0.5f;
            var waitTime = swingSpeed > 0 ? 1f / swingSpeed : 0f;

            for (float angle = -halfAngle; angle <= halfAngle; angle += 1f)
            {
                var horizontalRotation = Quaternion.AngleAxis(angle, Vector3.up);
                var directionAfterYaw = horizontalRotation * baseDirection;
                var tiltRotation = Quaternion.AngleAxis(tilt, sender.transform.right);
                var finalDirection = tiltRotation * directionAfterYaw;
                var ray = new Ray(sender.transform.position, finalDirection);
                var hits = Physics.RaycastAll(ray, range);

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
        }
        
    }
}