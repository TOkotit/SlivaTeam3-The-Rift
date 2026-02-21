using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Enums;
using MainCharacter;
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

        public void PerformAttack(RaycastAttackProfile profile, WeaponProfile weaponProfile,  GameObject sender, Teams team)
        {
            CastRaysContinous(profile,weaponProfile ,sender, team);
        }

        private IEnumerator CastRaysContinous(RaycastAttackProfile profile, WeaponProfile weaponProfile, GameObject sender, Teams team) 
        {
            var range = weaponProfile.Range;
            var damage = weaponProfile.Damage;
            var piercing = weaponProfile.Piercing;
            var swingSpeed = weaponProfile.SwingSpeed; 
            var totalAngle = profile.Angle;            
            var tilt = profile.Tilt;                    
            var baseDirection = sender.transform.forward;
            var halfAngle = totalAngle * 0.5f;
            var waitTime = swingSpeed > 0 ? 1f / swingSpeed : 0f;
            var hitTargets = new HashSet<DamagableModel>();

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
                    if (piercing && hitTargets.Contains(targetModel)) continue;

                    targetModel.Health.TakeDamage(Mathf.RoundToInt(damage), profile.DamageType);

                    if (piercing)
                        hitTargets.Add(targetModel);
                    else
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