using UnityEngine;

namespace MainCharacter
{
    
    // Пока просто создал заранее чтобы было. Конкретно этот файл брать не надо
    // Данные нужно брать из отдельного конфига в ресурсах через ResourcesLoad. Это делать будем в контейнере
    // Там автоматически сохраняются данные если их менять и эти данные подтягиваются в runTime
    // то есть можно балансить прям во время запуска без проблем
    // Для создания конфига нужно указать названия вот эти файлов и в Create будет доступно создание конфига
    
    // RE: Bruh
    
    [CreateAssetMenu(fileName = "MovementStats", menuName = "MainCharacter/MovementStatsSO")]
    public class MovementStatsSO : ScriptableObject
    {
        public readonly float _speed = 10;
        public readonly float _jumpHeight = 7;
        public readonly int _wallJumpCost = 10;
        public readonly int _dashCost = 20;
        public readonly float _dashSpeed = 50;
        public readonly float _dashTime = 0.1f;
        public readonly float _dashCooldown = 1f;
        public readonly int _wallJumpCount = 1;
    }
}