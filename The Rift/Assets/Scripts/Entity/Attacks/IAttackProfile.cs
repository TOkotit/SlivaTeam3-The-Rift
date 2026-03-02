using UnityEngine;

namespace Entity
{
    public interface IAttackProfile
    {
        string Name { get;  }
        float  Cooldown {get; }
        
    }
}