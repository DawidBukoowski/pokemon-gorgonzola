using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public event EventHandler JumpAction;

    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        JumpAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVector() {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
