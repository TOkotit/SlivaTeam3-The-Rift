using System.Collections.Generic;
using AYellowpaper;
using Entity.Attacks;
using UnityEngine;

namespace Entity
{
    public interface IAttackProfile
    {
        string Name { get;  }
        float  Cooldown {get; }
        
        List<InterfaceReference<IAttackEvent>> Events { get; } 
    }
}