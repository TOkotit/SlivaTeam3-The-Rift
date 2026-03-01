using Enums;
using MainCharacter;

namespace Entity
{
    public abstract class DamagableModel
    {
        protected Health _health;  
        public Health Health => _health;
        protected Teams _team;
        public Teams Team => _team;
    }
}