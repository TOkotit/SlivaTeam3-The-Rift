using System;
using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private NavMeshAgent agent;
        
        private float dashDuration;
        private float elapsed = 0f;
        private Vector3 dashStartPos;
        private Vector3 dashDirection;
        private Vector3 dashTargetPos;

        private bool isDashing;

        public bool IsDashing
        {
            get => isDashing;
            set => isDashing = value;
        }

        public void Dash(Direction directionType, float value, float duration)
        {
            agent.enabled = false;
            
            dashStartPos = transform.position;
            dashDirection = directionType switch
            {
                Direction.Forward => transform.forward,
                Direction.Backward => transform.forward * -1,
                Direction.Left => transform.right * -1,
                Direction.Right => transform.right * 1,
                _ => transform.forward
            };
        
            dashTargetPos = dashStartPos + dashDirection * value;
            dashDuration = duration;
            elapsed = 0f;
            isDashing = true;
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                if (elapsed >= dashDuration)
                {
                    transform.position = dashTargetPos;
                    StopDashing();
                }
                var newPosition = Vector3.Lerp(dashStartPos, dashTargetPos, elapsed / dashDuration);
        
                rigidbody.MovePosition(newPosition);
                elapsed += Time.fixedDeltaTime;
            }
        }

        public void StopDashing()
        {
            isDashing = false;
            agent.enabled = true;
        }
        
        public void MoveTo(Vector3 newPosition)
        {
            rigidbody.MovePosition(newPosition);
        }
    }
}