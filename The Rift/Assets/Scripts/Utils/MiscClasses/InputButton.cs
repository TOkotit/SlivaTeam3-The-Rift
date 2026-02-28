using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace Utils.MiscClasses
{
    [Serializable]
    public struct InputButton
    {
        public enum DeviceType { Keyboard, Mouse }
        public DeviceType device;
        public Key key;          
        public MouseButton mouse;
        
        public static InputButton Keyboard(Key key) => new InputButton { device = DeviceType.Keyboard, key = key }; 
        public static InputButton Mouse(MouseButton mouse) => new InputButton { device = DeviceType.Mouse, mouse = mouse };
    }
}