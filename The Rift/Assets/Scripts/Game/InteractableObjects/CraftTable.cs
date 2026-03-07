using Game.Gameplay.View.UI;
using UnityEngine;
using VContainer;

namespace Game
{
    public class CraftTable: MonoBehaviour, IInteractable
    {
        [Inject] public GameplayUIManager gameplayUIManager;
        
        public void Interact()
        {
            gameplayUIManager.OpenScreenForge();
        }
        public Transform InteractionPoint => transform;
    }
}