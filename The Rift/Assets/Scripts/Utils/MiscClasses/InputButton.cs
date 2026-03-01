
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


namespace Utils.MiscClasses
{
    [System.Serializable]
    public struct InputButton
    {
        public enum DeviceType { Keyboard, Mouse }
        public DeviceType device;
        public Key key;
        public MouseButton mouse;
        public bool hold;
        public float treshold;

        public bool IsPressed()
        {
            if (device == DeviceType.Keyboard)
                return Keyboard.current[key].wasPressedThisFrame;
            else
            {
                var mouseDev = Mouse.current;
                return mouse switch
                {
                    MouseButton.Left => mouseDev.leftButton.wasPressedThisFrame,
                    MouseButton.Right => mouseDev.rightButton.wasPressedThisFrame,
                    MouseButton.Middle => mouseDev.middleButton.wasPressedThisFrame,
                    _ => false
                };
            }
        }

        public bool IsHeld()
        {
            if (device == DeviceType.Keyboard)
                return Keyboard.current[key].isPressed;
            else
            {
                var mouseDev = Mouse.current;
                return mouse switch
                {
                    MouseButton.Left => mouseDev.leftButton.isPressed,
                    MouseButton.Right => mouseDev.rightButton.isPressed,
                    MouseButton.Middle => mouseDev.middleButton.isPressed,
                    _ => false
                };
            }
        }
    }
}