using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInput;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool dashInput;



    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
        
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.input.NormInputX;
        yInput = player.input.NormInputY;
        JumpInput = player.input.JumpInput;
        dashInput = player.input.DashInput;


        if (player.input.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        if (player.input.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        if (player.input.AttackInputs[(int)CombatInputs.fireElement])
        {
            stateMachine.ChangeState(player.FlameAttackState);
        }
        if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            if(PlayerEnergy.Instance.energy.Value == 0.1) return;
            PlayerEnergy.Instance.Use(player.playerEnergyCost.Value);
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    // protected void Move(Vector2 moveInput)
    // {
    //     Debug.Log("Move");
    //     xInput = moveInput.normalized.x;
    //     moveDirection = new Vector2 (xInput, 0);
    // }

}
