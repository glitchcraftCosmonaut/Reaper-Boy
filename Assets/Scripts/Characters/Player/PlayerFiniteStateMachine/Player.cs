using UnityEngine;
using static PlayerInput;

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
    public PlayerDashState DashState {get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    #endregion

    #region Setter and Getter
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    #endregion

    #region Scriptable object properties
    [Header("===PLAYER DATA===")]
    [SerializeField] public PlayerInput input;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerDashData playerDashData;

    [SerializeField] private EnemyBehaviourData enemyData;
    #endregion

    #region properties
    public Vector2 knockbackAngle;
    public float knockbackStrength;
    public Rigidbody2D RB { get; private set; }
    public Weapon[] weapons;
    #endregion

    Vector2 moveDirection;

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        sp = GetComponent<SpriteRenderer>();
        defaultMat2D = GetComponent<SpriteRenderer>().material;

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this,StateMachine,playerData,"InAir");
        InAirState = new PlayerInAirState(this,StateMachine,playerData,"InAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        DashState = new PlayerDashState(this, StateMachine, playerDashData, "InAir");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        
    }
    private void Start()
    {
        input.EnableGameplayInput();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        PrimaryAttackState.SetWeapon(weapons[(int)CombatInputs.primary]);
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
    public override void TakeDamage(float damage)
    {
        //this is work but improve this soon maybe
        base.TakeDamage(enemyData.attackDamage);
        IKnockbackable knockbackable = GetComponentInChildren<IKnockbackable>();
        knockbackable.Knockback(knockbackAngle, knockbackStrength, Core.Movement.FacingDirection * -1);       
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


}
