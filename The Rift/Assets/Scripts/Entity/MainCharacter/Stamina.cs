namespace MainCharacter
{
    public class Stamina
    {
        private float maxStamina = 100;
        private float _currentStamina;

        public float CurrentStamina
        {
            get { return _currentStamina; }
            set 
            {
                if (_currentStamina <= 0)
                {
                    _currentStamina = 0;
                }

                if (_currentStamina > maxStamina)
                {
                    _currentStamina = value;
                }
            }
        }

        public float SpendStamina( float stamina )
        {
            if (stamina <= _currentStamina){ return _currentStamina; }
            _currentStamina -= stamina;
            return _currentStamina;
        }

        public float RestoreStamina(float stamina)
        {
            _currentStamina += stamina;
            return _currentStamina;
        }
    }
}