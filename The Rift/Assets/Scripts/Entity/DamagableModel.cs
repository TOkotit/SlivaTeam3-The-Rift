using Enums;
using MainCharacter;

namespace Entity
{
    public abstract class DamagableModel
    {
        protected Health _health;  
        public Health Health
        {
            get => _health;
            set => _health = value;
        }
        protected Teams _team;
        public Teams Team => _team;
    }
}