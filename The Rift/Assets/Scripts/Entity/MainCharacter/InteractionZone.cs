using System.Collections.Generic;
using Game;
using UnityEngine;

namespace MainCharacter
{
    public class InteractionZone : MonoBehaviour
    {
        private List<IInteractable> _inRange = new();
        
        public List<IInteractable> InRange => _inRange;
        public IInteractable CurrentTarget => _inRange.Count > 0 ? _inRange[0] : null;
        private void OnTriggerEnter(Collider other) => TryAddInteractable(other);
        private void OnTriggerStay(Collider other) => TryAddInteractable(other);
        private void TryAddInteractable(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                if (other.TryGetComponent(out IInteractable interactable))
                {
                    if (!InRange.Contains(interactable))
                    {
                        InRange.Add(interactable);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                InRange.Remove(interactable);
            }
        }
    }
}