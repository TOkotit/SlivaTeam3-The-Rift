using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace Utils.MiscClasses
{
    [System.Serializable]
    public struct InputButton
    {
        public enum DeviceType { Keyboard, Mouse }
        public DeviceType device;
        public Key key;          
        public MouseButton mouse; 

        public bool IsPressed()
        {
            if (device == DeviceType.Keyboard)
            {
                return Keyboard.current[key].wasPressedThisFrame;
            }
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
    }
}