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

    #endregion

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }

    public CapsuleCollider2D MovementCollider { get; private set; }


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
    }
    private void Start()
    {
        input.EnableGameplayInput();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<CapsuleCollider2D>();
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
