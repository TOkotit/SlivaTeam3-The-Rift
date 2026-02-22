using Enums;
using MainCharacter;

namespace Entity
{
    public abstract class DamagableModel
    {
        protected Health health;  
        public Health Health => health;
        protected Teams team;
        public Teams Team => team;
    }
}