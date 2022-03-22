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
    private bool shootInput;

    float atkCooldown = 1f;
    float lastATK;



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
        shootInput = player.input.ShootInput;


        if (player.input.AttackInputs[(int)CombatInputs.primary])
        {
            AudioSetting.Instance.PlaySFX(player.slashSFX);
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        if (player.input.AttackInputs[(int)CombatInputs.secondary])
        {
            if(Time.time - lastATK < atkCooldown) return;
            lastATK = Time.time;
            AudioSetting.Instance.PlaySFX(player.slashSFX);
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        if (player.input.AttackInputs[(int)CombatInputs.fireElement] && player.hasFireAttack)
        {
            if(PlayerSpecialEnergy.Instance.specialEnergy.Value == 0) return;
            PlayerSpecialEnergy.Instance.Use(player.playerEnergyCost.Value);
            stateMachine.ChangeState(player.FlameAttackState);
        }
        if (player.input.AttackInputs[(int)CombatInputs.shootFire])
        {
            if(PlayerSpecialEnergy.Instance.specialEnergy.Value == 0) return;
            stateMachine.ChangeState(player.ShootFireState);
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
            if(PlayerEnergy.Instance.energy.Value == 0) return;
            AudioSetting.Instance.PlaySFX(player.dashSFX);
            PlayerEnergy.Instance.Use(player.playerEnergyCost.Value);
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CheckIfCanSlash()
    {
        // return CanDash && player.playerDashData.hasDash && Time.time >= lastDashTime + playerDashData.dashCooldown;
        return Time.time - lastATK < atkCooldown;
    }
    

}
