using System.Collections.Generic;

namespace Game.Inventory
{
    public class Inventory
    {
        //предмет - количество
        public Dictionary<Item, int> items;
        public Dictionary<Rune, int> runes;

        public void AddItem(Item item)
        {
            if (!items.TryAdd(item, 1))
            {
                items[item]++;
            }
        }
        
        public void AddRune(Rune rune)
        {
            if (!runes.TryAdd(rune, 1))
            {
                runes[rune]++;
            }
        }
    }
}