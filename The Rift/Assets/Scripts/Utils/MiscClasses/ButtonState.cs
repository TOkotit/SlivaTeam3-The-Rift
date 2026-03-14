using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.MiscClasses;

public class ButtonState
{
    public InputButton Button { get; }
    public InputAction Action { get; }
    public Coroutine HoldCoroutine { get; set; }
    public bool HoldTriggered { get; set; }

    private Action<InputAction.CallbackContext> _startedHandler;
    private Action<InputAction.CallbackContext> _canceledHandler;

    public ButtonState(InputButton button, InputAction action)
    {
        Button = button;
        Action = action;
    }

    public void Subscribe(Action<InputButton> onStarted, Action<InputButton> onCanceled)
    {
        _startedHandler = _ => onStarted(Button);
        _canceledHandler = _ => onCanceled(Button);

        Action.started += _startedHandler;
        Action.canceled += _canceledHandler;
        Action.Enable();
    }

    public void Unsubscribe()
    {
        if (_startedHandler != null)
        {
            Action.started -= _startedHandler;
            _startedHandler = null;
        }
        if (_canceledHandler != null)
        {
            Action.canceled -= _canceledHandler;
            _canceledHandler = null;
        }
    }
}