using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerInput;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState {get; private set; }
    public PlayerDashState DashState {get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    public PlayerAttackState FlameAttackState { get; private set; }
    public PlayerAttackState ShootFireState {get; private set;}

    #endregion

    #region Setter and Getter
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    // public HashSet<string> UpgradeStates { get; set; } = new HashSet<string>();

    #endregion

    #region Scriptable object properties
    [Header("===PLAYER DATA===")]
    [SerializeField] public PlayerInput input;
    [SerializeField] public PlayerData playerData;
    [SerializeField] public PlayerDashData playerDashData;
    [SerializeField] public FloatValueSO playerEnergyCost;
    [SerializeField] private EnemyBehaviourData enemyData;
    #endregion

    #region properties
    
    [SerializeField] bool regenerateEnergy = true;
    [SerializeField] float energyRegenerateTime;
    [SerializeField,Range(0f, 1f)] float energyRegeneratePercent;
    Coroutine energyRegenerateCoroutine;
    WaitForSeconds waitEnergyRegenerateTime;

     //SFX
    public AudioData dashSFX;
    public AudioData shootSFX;
    public AudioData slashSFX;


    private Vector2 knockbackAngle;
    private float knockbackStrength;
    private Rigidbody2D rb;
    public Rigidbody2D RB { get; private set; }
    [SerializeField] GameObject projectile;
    public GameObject dashAfterImage;
    [SerializeField] Transform muzzleTop;
    public Weapon[] weapons;

    bool isInvincible;
    // [SerializeField] float shootCooldownTime = 1f;

    // public bool isShootReady = true;


    public bool hasDash = false;
    public bool hasFireAttack = false;
    public Vector2 boxSize = new Vector2(0.1f,1f);
    public Scene sceneName;
    public string stageName;

    #endregion

    Vector2 moveDirection;
    public static Player instance;
    public static Player MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        #region Component
        Core = GetComponentInChildren<Core>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
        defaultMat2D = GetComponentInChildren<SpriteRenderer>().material;
        stageName = SceneManager.GetActiveScene().name;
        waitEnergyRegenerateTime = new WaitForSeconds(energyRegenerateTime);


        #endregion


        #region statemachine
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this,StateMachine,playerData,"InAir");
        InAirState = new PlayerInAirState(this,StateMachine,playerData,"InAir");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "InAir");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "WallSlide");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        DashState = new PlayerDashState(this, StateMachine, playerDashData, "Dash");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        FlameAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        ShootFireState = new PlayerAttackState(this, StateMachine,playerData,"attack");
        #endregion
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        health.Value = 1;
        input.onInteract += CheckInteraction;
        input.onShootFire += ShootFire;
        // input.onStopShootFire += ShootFire;
        sp.material = defaultMat2D;
        if(gameObject.activeSelf)
        {
            if(regenerateEnergy)
            {
                // if(energyRegenerateCoroutine != null)
                // {
                //     StopCoroutine(energyRegenerateCoroutine);
                // }
                energyRegenerateCoroutine = StartCoroutine(PlayerEnergy.Instance.EnergyRegenCoroutine(waitEnergyRegenerateTime, energyRegeneratePercent));
            }
        }
    }
    private void OnDisable()
    {
        input.onInteract -= CheckInteraction;
        input.onShootFire -= ShootFire;
        input.onStopShootFire -= ShootFire;
    }
    
    private void Start()
    {
        // name = playerDashData.objectName; 
        sceneName = SceneManager.GetSceneByName(stageName);
        input.EnableGameplayInput();
        // hasDash = playerDashData.hasDash;
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        PrimaryAttackState.SetWeapon(weapons[(int)CombatInputs.primary]);
        SecondaryAttackState.SetWeapon(weapons[(int)CombatInputs.secondary]);
        FlameAttackState.SetWeapon(weapons[(int)CombatInputs.fireElement]);
        ShootFireState.SetWeapon(weapons[(int)CombatInputs.shootFire]);
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
        if(gameObject.activeSelf && !isInvincible)
        {
            StartCoroutine(FlashAfterDamage());
            base.TakeDamage(enemyData.attackDamage);
            Invincible(true);
            rb.velocity = Vector2.up * 15;
        }  
    }
    public override void Die()
    {
        GameManager.onGameOver?.Invoke();
        GameManager.GameState = GameState.GameOver;
        PoolManager.Release(deathVFX, transform.position);
        base.Die();
        gameObject.SetActive(false);
    }


    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if(rc.transform.GetComponent<InteractionSystem>())
                {
                    rc.transform.GetComponent<InteractionSystem>().Interact();
                    return;
                }
            }
        }
    }

    private IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.0833f;
        // toggle transparency
        for (int i = 0; i < 10; i++)
        {
            sp.color = Color.clear;
            yield return new WaitForSeconds(flashDelay);
            sp.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }
        // no longer invincible
        Invincible(false);
    }

    public void Invincible(bool invincibility)
    {
        isInvincible = invincibility;
    }

    public void ShootFire()
    {
        // StartCoroutine(nameof(FireCoroutine));
        // PoolManager.Release(missilePrefab, muzzleTransform.position);
        if(PlayerSpecialEnergy.Instance.specialEnergy.Value == 0) return;
        // isShootReady = false;
        PoolManager.Release(projectile, muzzleTop.position, muzzleTop.rotation);
        PlayerSpecialEnergy.Instance.Use(playerEnergyCost.Value);
        AudioSetting.Instance.PlaySFX(shootSFX);
        // if(PlayerSpecialEnergy.Instance.specialEnergy.Value > 0)
        // {
        //     StartCoroutine(CoolDownCoroutine());
        // }
    }
    // public IEnumerator CoolDownCoroutine()
    // {
    //     var cooldownValue = shootCooldownTime;

    //     while(cooldownValue > 0f)
    //     {
    //         cooldownValue = Mathf.Max(cooldownValue - Time.deltaTime, 0f);

    //         yield return null;
    //     }

    //     isShootReady = true;
    // }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


}
