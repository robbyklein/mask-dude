using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "InputHandler", menuName = "ScriptableObjects/InputHandler")]
public class InputHandlerSO : ScriptableObject {
    private PlayerInputActions input;

    private InputMap activeMap = InputMap.None;

    public enum InputMap {
        None,
        Menu,
        Game,
    }

    #region Events
    public Action OnMenuLeft;
    public Action OnMenuRight;
    public Action OnMenuUp;
    public Action OnMenuDown;
    public Action OnMenuBack;
    public Action OnMenuSelect;
    public Action<Vector2> OnGameMovement;
    public Action OnGameJump;
    public Action<bool> OnGameFiringChange;

    #endregion

    #region Lifecycle
    private void OnEnable() {
        input ??= new PlayerInputActions();

        SubscribeToActions();
        EnableInitialInputMap();
    }

    private void OnDisable() {
        UnsubscribeToActions();
    }

    private void SubscribeToActions() {
        input.Menus.Up.performed += HandleMenusUp;
        input.Menus.Down.performed += HandleMenusDown;
        input.Menus.Left.performed += HandleMenusLeft;
        input.Menus.Right.performed += HandleMenusRight;
        input.Menus.Back.performed += HandleMenusBack;
        input.Menus.Select.performed += HandleMenusSelect;

        input.Game.Movement.performed += HandleGameMovement;
        input.Game.Movement.canceled += HandleGameMovement;
        input.Game.Jump.performed += HandleGameJump;
        input.Game.Fire.performed += HandleGameFire;
        input.Game.Fire.canceled += HandleGameFire;

    }

    private void UnsubscribeToActions() {
        input.Menus.Up.performed -= HandleMenusUp;
        input.Menus.Down.performed -= HandleMenusDown;
        input.Menus.Left.performed -= HandleMenusLeft;
        input.Menus.Right.performed -= HandleMenusRight;
        input.Menus.Back.performed -= HandleMenusBack;
        input.Menus.Select.performed -= HandleMenusSelect;

        input.Game.Movement.started -= HandleGameMovement;
        input.Game.Movement.canceled -= HandleGameMovement;
        input.Game.Jump.performed -= HandleGameJump;
        input.Game.Fire.performed -= HandleGameFire;
        input.Game.Fire.canceled -= HandleGameFire;
    }
    #endregion

    #region Menu input
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
    #endregion

    #region Game input
    private void HandleGameMovement(InputAction.CallbackContext context) {
        Vector2 movementInput = context.ReadValue<Vector2>();
        OnGameMovement?.Invoke(movementInput);
    }

    private void HandleGameJump(InputAction.CallbackContext context) {
        OnGameJump?.Invoke();
    }

    private void HandleGameFire(InputAction.CallbackContext context) {
        float pressed = context.ReadValue<float>();
        OnGameFiringChange?.Invoke(pressed > 0.1f);
    }
    #endregion

    #region Helpers
    private void EnableInitialInputMap() {
#if UNITY_EDITOR
        Scene activeScene = SceneManager.GetActiveScene();
        string sceneName = activeScene.name;

        if (sceneName == "Game") {
            ChangeInputMap(InputMap.Game);
        } else {
            ChangeInputMap(InputMap.Menu);
        }
#endif
    }

    public void DisableInput() {
        if (activeMap != InputMap.None) DisableInputMap(activeMap);
    }

    public void ChangeInputMap(InputMap map) {
        if (activeMap != InputMap.None) DisableInputMap(activeMap);
        EnableInputMap(map);
    }

    private void EnableInputMap(InputMap map) {
        switch (map) {
            case InputMap.Menu:
                input.Menus.Enable();
                activeMap = InputMap.Menu;
                break;
            case InputMap.Game:
                Debug.Log("Enabling game!");
                input.Game.Enable();
                activeMap = InputMap.Game;
                break;
        }
    }

    private void DisableInputMap(InputMap map) {
        switch (map) {
            case InputMap.Menu:
                input.Menus.Disable();
                break;
            case InputMap.Game:
                input.Game.Disable();
                break;
        }
    }
    #endregion
}
