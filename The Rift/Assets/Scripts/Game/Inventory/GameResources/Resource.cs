using R3;

namespace Game.Inventory
{
    /// <summary>
    /// Реактивная оболочка над ресурсами чтобы при изменении количества обновлять UI и тд
    /// </summary>
    public class Resource
    {
        public ResourceData Origin;
        public ReactiveProperty<int> Amount;
        
        public ResourceType ResourceType => Origin.resourceType;

        public Resource(ResourceData origin)
        {
            Origin = origin;
            Amount = new ReactiveProperty<int>(origin.amount);
            //тут связываем количество с классом с данными 
            Amount.Subscribe(onNext: newValue => origin.amount = newValue );
        }
    }
}