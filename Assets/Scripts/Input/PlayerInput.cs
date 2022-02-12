using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IGameplayActions
{

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    // public event UnityAction onJump = delegate {};

    InputActions inputActions;

    private void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.Gameplay.SetCallbacks(this);
    }
    private void OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs()
    {
        inputActions.Gameplay.Disable();
    }

    public void EnableGameplayInput()
    {
        inputActions.Gameplay.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // if(context.performed)
        // {
        //     onJump.Invoke();
        // }
    }
}
