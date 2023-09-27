using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputHandler", menuName = "ScriptableObjects/InputHandler")]
public class InputHandlerSO : ScriptableObject {
    private PlayerInputActions input;
    public Action OnMenuLeft;
    public Action OnMenuRight;
    public Action OnMenuUp;
    public Action OnMenuDown;
    public Action OnMenuBack;
    public Action OnMenuSelect;

    private void OnEnable() {
        input ??= new PlayerInputActions();

        SubscribeToActions();
        EnableDefaultInputMap();
    }

    private void OnDisable() {
        UnsubscribeToActions();
    }

    private void EnableDefaultInputMap() {
        input.Menus.Enable();
    }

    private void SubscribeToActions() {
        input.Menus.Up.performed += HandleMenusUp;
        input.Menus.Down.performed += HandleMenusDown;
        input.Menus.Left.performed += HandleMenusLeft;
        input.Menus.Right.performed += HandleMenusRight;
        input.Menus.Back.performed += HandleMenusBack;
        input.Menus.Select.performed += HandleMenusSelect;
    }

    private void UnsubscribeToActions() {
        input.Menus.Up.performed -= HandleMenusUp;
        input.Menus.Down.performed -= HandleMenusDown;
        input.Menus.Left.performed -= HandleMenusLeft;
        input.Menus.Right.performed -= HandleMenusRight;
        input.Menus.Back.performed -= HandleMenusBack;
        input.Menus.Select.performed -= HandleMenusSelect;
    }

    private void HandleMenusUp(InputAction.CallbackContext context) {
        OnMenuUp?.Invoke();
    }

    private void HandleMenusRight(InputAction.CallbackContext context) {
        OnMenuRight?.Invoke();
    }

    private void HandleMenusLeft(InputAction.CallbackContext context) {
        OnMenuLeft?.Invoke();
    }

    private void HandleMenusDown(InputAction.CallbackContext context) {
        OnMenuDown?.Invoke();
    }

    private void HandleMenusBack(InputAction.CallbackContext context) {
        OnMenuBack?.Invoke();
    }

    private void HandleMenusSelect(InputAction.CallbackContext context) {
        OnMenuSelect?.Invoke();
    }
}
