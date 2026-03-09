using System.Collections.Generic;
using System.Linq;
using Entity.Runes;
using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "RuneDatabase", menuName = "Runes/Database")]
    public class RuneDatabase : ScriptableObject
    {
        public List<RuneData> allRunes;

        public RuneData GetRuneByType(RuneType type) 
            => allRunes.FirstOrDefault(r => r.Rune == type);

        public RuneData GetRuneById(string id) 
            => allRunes.FirstOrDefault(r => r.runeName == id);
    }
}