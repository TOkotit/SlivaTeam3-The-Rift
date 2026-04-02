using MainCharacter;
using Systems;
using VContainer;

namespace Utils
{
    public class MainCharacterProvider
    {
        [Inject]
        private MainCharacterModel mainCharacter;
        public MainCharacterModel MainCharacter => mainCharacter;
        [Inject]
        private ParrySystem parrySystem;
        public ParrySystem ParrySystem => parrySystem;
    }
}