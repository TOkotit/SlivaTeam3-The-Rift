using System;
using Game.Inventory;

namespace Game.Gameplay
{
    [Serializable]
    public class GameData
    {
        public InventoryModel _inventoryModel;


        public GameData()
        {
            _inventoryModel = new InventoryModel();
        }
    }
}