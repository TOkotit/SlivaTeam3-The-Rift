using UnityEngine;

namespace Entity
{
    public abstract class Character : MonoBehaviour
    {
        public abstract CharacterModel CharacterModel {get; }
    }
}