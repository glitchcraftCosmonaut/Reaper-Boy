using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IGameplayActions, InputActions.IPauseMenuActions
{

#region Encapsulasion Properties
    public event UnityAction onPause = delegate {};
    public event UnityAction onUnPause = delegate {};
    public event UnityAction onInteract = delegate {};
    public event UnityAction onShootFire = delegate {};
    public event UnityAction onStopShootFire = delegate {};

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool ShootInput {get; private set;}
    public bool ShootInputStop {get; private set;}
    public bool[] AttackInputs { get; private set; }


#endregion

#region Properties
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float dashInputStartTime;
    InputActions inputActions;
    public CombatInputs combatInputs;
    // public int primary;
    // public int secondary;
#endregion

#region UNITY MONOBEHAVIOR
    private void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.Gameplay.SetCallbacks(this);
        inputActions.PauseMenu.SetCallbacks(this);

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }
    private void OnDisable()
    {
        DisableAllInput();

    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }
#endregion

#region INPUT HANDLER

    void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        inputActions.Disable();
        actionMap.Enable();

        if(isUIInput)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void SwitchToDynamicUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    public void SwitchToFixedUpdateMode() => InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    public void DisableAllInput() => inputActions.Disable();

    public void EnableGameplayInput() => SwitchActionMap(inputActions.Gameplay, false);
    public void EnablePauseInput() => SwitchActionMap(inputActions.PauseMenu, true);
    
#endregion

#region MOVEMENT INPUT

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
#endregion

#region JUMP INPUT
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if(context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

#endregion

#region DASH INPUT

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void UseDashInput() => DashInput = false;


    #endregion

#region ATTACK INPUT
    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnFireAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.fireElement] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.fireElement] = false;
        }
    }

    public void OnShootingFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            AttackInputs[(int)CombatInputs.shootFire] = true;
            onShootFire.Invoke();
        }
        if(context.canceled)
        {
            AttackInputs[(int)CombatInputs.shootFire] = false;
            onStopShootFire.Invoke();
        }
    }
    public void UseShootInput() => ShootInput = false;

#endregion

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            onPause.Invoke();
        }
    }

    public void OnUnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            onUnPause.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            onInteract.Invoke();
        }
    }


    public enum CombatInputs
    {
        primary,
        secondary,
        fireElement,
        shootFire
    }
}
