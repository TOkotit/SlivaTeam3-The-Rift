using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainCharacter
{
    public class MainCharacterFactory
    {
        private readonly MainCharacter _characterPrefab;
        private readonly IObjectResolver _container;

        public MainCharacterFactory(MainCharacter characterPrefab, IObjectResolver container)
        {
            _characterPrefab = characterPrefab;
            _container = container;
        }

        public MainCharacter CreateMainCharacter(MovementStatsSO stats, Vector3 position)
        {
            var newObject = Object.Instantiate(_characterPrefab.gameObject, position, Quaternion.identity);
            var character = newObject.GetComponent<MainCharacter>();
            _container.InjectGameObject(newObject);
            var model = character.MainCharacterModel;
            model.DashCooldown = stats._dashCooldown;
            model.DashSpeed = stats._dashSpeed;
            model.DashTime = stats._dashTime;
            model.DashCost = stats._dashCost;
            model.WallJumpCount = stats._wallJumpCount;
            model.WallJumpCost = stats._wallJumpCost;
            model.Speed = stats._speed;
            model.JumpHeight = stats._jumpHeight;
            return character;
        }
    }
}