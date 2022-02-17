using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected PlayerDashData playerDashData;


    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;
    
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.Core;
    }
    public PlayerState(Player player, PlayerStateMachine stateMachine,PlayerDashData playerDashData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.playerDashData = playerDashData;
        core = player.Core;
    }

    // public virtual void OnEnable()
    // {
    //     Enter();
    // }

    // Entering state when get input feedback from player, initial state is going idle
    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        //Debug.Log(animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    // Exit state when change input or going idle
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    //method for using Update method from monobehaviour all related to input are going here
    public virtual void LogicUpdate()
    {

    }
     //same as using logic update but this method using FixedUpdate
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    //Checking if the player can change state or not
    public virtual void DoChecks() { }

    //using this for animation  trigger in unity editor
    public virtual void AnimationTrigger() { }

    //using this for animation finish trigger in unity editor
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
