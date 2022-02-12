using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    protected Vector2 moveDirection;
    private bool JumpInput;
    private bool isGrounded;


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
