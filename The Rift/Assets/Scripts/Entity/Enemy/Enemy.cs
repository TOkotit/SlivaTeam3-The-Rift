using MainCharacter;
using UnityEngine;
using VContainer;

namespace Entity.Enemy
{
    public class Enemy : Character
    {
        [Inject] private EnemyModel _enemyModel;
        public override CharacterModel CharacterModel => _enemyModel;
        public EnemyModel EnemyModel => _enemyModel;
    }
}