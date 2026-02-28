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
        public float _speed;
        public LayerMask _groundMask;
        public Transform _groundCheck;
        public float _groundCheckRadius;
        public float _jumpHeight;
    }
}