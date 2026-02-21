using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Enums;
using MainCharacter;
using UnityEngine;
using UnityEngine.UIElements;

namespace Systems
{
    public class AttackSystem
    {
        static AttackSystem instance;

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

        public void PerformAttack(RaycastAttackProfile profile, WeaponProfile weaponProfile,  GameObject sender)
        {
            CastRaysContinous(profile,weaponProfile ,sender);
        }

        private IEnumerator CastRaysContinous(RaycastAttackProfile profile, WeaponProfile weaponProfile, GameObject sender) 
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

            HashSet<GameObject> hitTargets = new HashSet<GameObject>();

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
                    var target = hit.collider.gameObject;

                    var model = target.GetComponent<Character>().CharacterModel;  // переделать с вызовом конкретного комопонента
                                                                        // Сделать модификаторы дальности и урона от профилья самой атаки
                    if (model == null) continue;                       
                    if (model.Team == sender.GetComponent<Character>()?.CharacterModel.Team) continue;
                    if (piercing && hitTargets.Contains(target)) continue;

                    model.Health.TakeDamage(damage, profile.DamageType);

                    if (piercing) hitTargets.Add(target);

                    if (!piercing) yield break; 
                }

                if (waitTime > 0f)
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
        }
        
    }
}