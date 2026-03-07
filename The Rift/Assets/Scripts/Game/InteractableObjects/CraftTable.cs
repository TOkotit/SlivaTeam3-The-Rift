using UnityEngine;

namespace Game
{
    public class CraftTable: MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interact");
        }
        public Transform InteractionPoint => transform;
    }
}