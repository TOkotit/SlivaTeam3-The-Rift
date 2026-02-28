using UnityEngine;

namespace Entity.Enemy.Warrior
{
    public class Warrior : Enemy
    {
        //Область в которой противник засекает персонажа, не видя напрямую
        [SerializeField] private Transform _proximityAreaCenter;
        [SerializeField] private float _proximityAreaRadius;
        
        //Поле зрения в котором противник видит персонажа, намного больше proximityArea
        [SerializeField] private GameObject _sightArea;
        
        
        
        
        
        
        public void FindCharacter()
        {
            
        }
    }
}