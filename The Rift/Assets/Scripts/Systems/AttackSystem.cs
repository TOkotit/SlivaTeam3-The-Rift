using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Entity.Attacks;
using Entity.Enemy;
using Enums;
using MainCharacter;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Utils.MiscClasses;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Systems
{
    public class AttackSystem
    {
        static AttackSystem instance;

        [Inject] private readonly DamagableRegistry _registry;
        [Inject] private ICoroutineRunner _coroutineRunner;
        [Inject] ParrySystem _parrySystem;
        [Inject] private readonly IObjectResolver _container;

        public Action<DamagableModel> OnHit;

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
            private set { instance = value; }
        }

        public void PerformAttack(IAttackProfile profile, Weapon weaponProfile, GameObject sender, Teams team)
        {
            profile.Events.ForEach(e => e.Value.Act());

            if (profile is RaycastAttackProfile raycastProfile)
            {
                Vector3 rayOrigin = sender.transform.position + sender.transform.rotation * raycastProfile.Offset;
                _coroutineRunner.StartRoutine(
                    CastRaysContinuous(raycastProfile, weaponProfile, sender, team)
                );
            }

            if (profile is CompositeAttackProfile compositeProfile)
            {
                foreach (var timing in compositeProfile.AttackTimings)
                {
                    ///
                }
            }

            if (profile is AttackSwitch attackSwitch)
            {
                PerformAttack(attackSwitch.GetNextAttack(), weaponProfile, sender, team);
            }

            if (profile is ObjectAttackProfile objectAttackProfile)
            {
                Vector3 worldOffset = sender.transform.rotation * objectAttackProfile.Offset;
                GameObject.Instantiate(
                    objectAttackProfile.Object,
                    sender.transform.position + worldOffset,
                    sender.transform.rotation
                );
            }

            if (profile is ProjectileAttackProfile projectileProfile)
            {
                Vector3 worldOffset = sender.transform.rotation * projectileProfile.Offset;
                Projectile projectile = _container.Instantiate(
                    projectileProfile.Projectile,
                    sender.transform.position + worldOffset,
                    sender.transform.rotation
                );
                projectile.Launch(sender.transform.forward, projectileProfile.Power);
            }

            if (profile is CircularAttackProfile circularProfile)
            {
                Vector3 center = sender.transform.position + sender.transform.rotation * circularProfile.Offset;
                float halfHeight = circularProfile.Metrics.y * 0.5f;

                Vector3 axis = sender.transform.forward;
                Quaternion tiltRotation = Quaternion.AngleAxis(circularProfile.Tilt, axis);
                Vector3 worldAxis = tiltRotation * sender.transform.up;

                Vector3 point1 = center + worldAxis * halfHeight;
                Vector3 point2 = center - worldAxis * halfHeight;

                Collider[] colliders = Physics.OverlapCapsule(point1, point2, circularProfile.Metrics.x);

                #region DrawCapsule (временная)

                GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                Object.Destroy(capsule.GetComponent<Collider>());
                capsule.transform.position = (point1 + point2) * 0.5f;
                capsule.transform.rotation = Quaternion.LookRotation(worldAxis, Vector3.up);
                float height = Vector3.Distance(point1, point2);
                capsule.transform.localScale = new Vector3(circularProfile.Metrics.x * 2f, height * 0.5f,
                    circularProfile.Metrics.x * 2f);
                capsule.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
                GameObject.Destroy(capsule, 0.1f);

                #endregion

                foreach (var col in colliders)
                {
                    var target = _registry.TryGetCharacter(col.gameObject);
                    if (target == null) continue;
                    if (target.Team == team) continue;
                    target.Health.TakeDamage(weaponProfile.Model.Damage, circularProfile.DamageType);

                    if (!circularProfile.Piercing) break;
                }
            }
        }


        private IEnumerator CastRaysContinuous(RaycastAttackProfile attackProfile, Weapon weaponProfile,
            GameObject sender, Teams team)
        {
            var range = weaponProfile.Model.Range * attackProfile.DistanceMultiplier;
            var damage = weaponProfile.Model.Damage * attackProfile.DamageMultiplier;
            var piercing = weaponProfile.Model.Piercing;
            var swingSpeed = weaponProfile.Model.SwingSpeed;
            var totalAngle = attackProfile.Angle;
            var tilt = attackProfile.Tilt;
            var halfAngle = totalAngle * 0.5f;
            var waitTime = (swingSpeed > 0 ? 1f / swingSpeed : 0f) / totalAngle;
            var hitAnybody = false;
            GameObject enemyGameObject = null;
            var oncePerAttackHittedModels = new HashSet<DamagableModel>(); 
            Debug.Log("CastRaysContinuous started");
            for (var angle = -halfAngle; angle <= halfAngle; angle += 1f)
            {
                var baseDirection = sender.transform.forward;
                var horizontalRotation = Quaternion.AngleAxis(angle, Vector3.up);
                var directionAfterYaw = horizontalRotation * baseDirection;
                var tiltRotation = Quaternion.AngleAxis(tilt, sender.transform.forward);
                var finalDirection = tiltRotation * directionAfterYaw;
                var ray = new Ray(sender.transform.position, finalDirection);
                var hits = Physics.RaycastAll(ray, range);

                #region DrawRays

                // начало отрисовки
                var endPoint = ray.origin + ray.direction * range;
                var lineObj = new GameObject("AttackRay");
                var lr = lineObj.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, endPoint);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.startColor = Color.red;
                lr.endColor = Color.red;
                CoroutineRunner.Destroy(lineObj, 0.1f);
                //конец отрисовки

                #endregion
                // var hittedModels = new List<DamagableModel>();
                
                foreach (var hit in hits)
                {
                    var targetModel = _registry.TryGetCharacter(hit.collider.gameObject);
                    if (!oncePerAttackHittedModels.Contains(targetModel))
                    {
                        OnHit?.Invoke(targetModel);
                        targetModel?.TakeHit();
                    }
                    oncePerAttackHittedModels.Add(targetModel);
                    if (targetModel == null) continue;
                    if (targetModel.Team == team) continue;
                    targetModel.Health.TakeDamage(Mathf.RoundToInt(damage), attackProfile.DamageType);
                    if (!hitAnybody)
                    {
                        enemyGameObject = hit.collider.gameObject;
                        hitAnybody = true;
                    }
                    if (!piercing)
                        yield break;

                }

                if (waitTime > 0f) yield return new WaitForSeconds(waitTime);
            }
            
            weaponProfile.Damage(1); // перенёс сюда, чтобы тратилось только при попадании
            if (!enemyGameObject)
            {
                var mainCharacter = _container.Resolve<MainCharacter.MainCharacter>();
                weaponProfile.Model.RegisterHit(null, mainCharacter.transform.position + mainCharacter.transform.forward, hitAnybody);
            }
            else
            {
                weaponProfile.Model.RegisterHit(enemyGameObject, enemyGameObject.transform.position, hitAnybody); // регестрирую попадание чтобы руна сработала
            }
                
            Debug.Log("CastRaysContinuous finished" );
        }
        
        private GameObject _tempAttackBox;
        public void PerformEnemyAttack(EnemyAttackData attackData, EnemyModel enemyModel, GameObject sender)
        {
            if (attackData is CloseEnemyAttackData closeAttackData)
            {
                _tempAttackBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _tempAttackBox.transform.position = sender.transform.position + sender.transform.rotation * closeAttackData.Offset;
                _tempAttackBox.transform.rotation = sender.transform.rotation;
                _tempAttackBox.transform.localScale = closeAttackData.Metrics;
                GameObject.Destroy(_tempAttackBox.GetComponent<Collider>());
                var renderer = _tempAttackBox.GetComponent<Renderer>();
                renderer.material.color = new Color(1, 0, 0, 0.3f);
    
                Collider[] colliders = Physics.OverlapBox(
                    sender.transform.position + sender.transform.rotation * closeAttackData.Offset,
                    closeAttackData.Metrics / 2,
                    sender.transform.rotation
                );
    
                DamagableModel playerModel = null;
                bool hitPlayer = false;
                foreach (var col in colliders)
                {
                    if (!col.CompareTag("Player")) continue;
                    playerModel = _registry.TryGetCharacter(col.gameObject);
                    if (playerModel != null && playerModel.Team != Teams.Enemy)
                    {
                        hitPlayer = true;
                        break;
                    }
                }

                if (hitPlayer && _parrySystem.IsParrying)
                {
                    Debug.Log("Attack parried!");
                    ParrySystem._onParry?.Invoke(enemyModel);
                }
                else if (hitPlayer)
                {
                    playerModel.Health.TakeDamage(closeAttackData.Damage, closeAttackData.DamageType);
                }
    
                GameObject.Destroy(_tempAttackBox, 0.1f);
            }
        }

        private IEnumerator CompositeAttackCoroutine(CompositeAttackProfile.AttackTiming timing, Weapon weapon, GameObject sender, Teams team)
        {
            PerformAttack(timing.Attack.Value, weapon, sender, team);
            yield return new WaitForSeconds(timing.Timing);
        }
    }
}