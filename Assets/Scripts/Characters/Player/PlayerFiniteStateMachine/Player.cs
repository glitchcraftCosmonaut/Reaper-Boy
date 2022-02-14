using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState {get; private set; }

    #endregion

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }

    public BoxCollider2D MovementCollider { get; private set; }


    [SerializeField] public PlayerInput input;
    [SerializeField] private PlayerData playerData;

    public Rigidbody2D RB { get; private set; }


    Vector2 moveDirection;

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this,StateMachine,playerData,"InAir");
        InAirState = new PlayerInAirState(this,StateMachine,playerData,"InAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
    }
    private void Start()
    {
        input.EnableGameplayInput();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        StateMachine.Initialize(IdleState); 
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    // protected override void OnEnable()
    // {
    //     base.OnEnable();
    //     Core = GetComponentInChildren<Core>();
    //     input.onMove += Move;    

    // }

    // private void OnDisable()
    // {
    //     input.onMove -= Move;
    // }
    // void Move(Vector2  moveInput)
    // {
    //     moveDirection = moveInput.normalized;
    //     Debug.Log(moveDirection);
    // }
}
