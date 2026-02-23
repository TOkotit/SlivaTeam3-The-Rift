using Enums;
using R3;

namespace Game.Inventory.InventoryItems
{
    /// <summary>
    /// Реактивная оболочка над ресурсами чтобы при изменении количества обновлять UI и тд
    /// </summary>
    public class Item
    {
        public ItemData Origin;
        public ReactiveProperty<int> Amount;
        public string Name => Origin.name;
        public string Description  => Origin.description;
        public int Cost  => Origin.cost;
        public ItemsCategory ItemsCategory => Origin.itemsCategory;

        public Item(ItemData origin)
        {
            Origin = origin;
            Amount = new ReactiveProperty<int>(origin.amount);
            //тут связываем количество с классом с данными 
            Amount.Subscribe(onNext: newValue => origin.amount = newValue );
        }
    }
}