using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Runes
{
    [CreateAssetMenu(fileName = "New Rune", menuName = "Runes/Rune")]
    public class RuneData : ScriptableObject
    {
        public string runeName;
        public Sprite icon;
        public RuneType Rune;
        [Tooltip("Список всех параметров, которые меняет эта руна")]
        public List<RuneModifier> Modifiers = new();
        
        
    }
    
    [Serializable] 
    public struct RuneModifier
    {
        public Influence Parameter; 
        public float Coefficient;
    }
}