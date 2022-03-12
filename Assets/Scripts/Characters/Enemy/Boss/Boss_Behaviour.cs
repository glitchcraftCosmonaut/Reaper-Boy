using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInput;

public class Boss_Behaviour : Character, IDamageable
{
    public PlayerSaveData data { get; set; } = new PlayerSaveData();
    public EnemyBehaviourData enemyData;
    BossHealthBar healthBar;

    // EnemySave enemySave;
    
    UniqueID uniqueID;
    Animator bossAnim;
    Rigidbody2D bossRB;
    Collider2D bossCollider;
    public Transform target;
    public GameObject tPlayer;
    public bool isDeath = false;
    public float speed;
    [HideInInspector] public bool enableAct;
    [HideInInspector] public Canvas healthBarCanvas;
    int atkStep;

    public static Boss_Behaviour instance;
    public static Boss_Behaviour MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Boss_Behaviour>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        healthBar = FindObjectOfType<BossHealthBar>();
        healthBarCanvas = healthBar.GetComponentInChildren<Canvas>();
        bossAnim = GetComponent<Animator>();
        bossRB = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
        uniqueID = GetComponent<UniqueID>();
        sp = GetComponentInChildren<SpriteRenderer>();
        defaultMat2D = GetComponentInChildren<SpriteRenderer>().material;
        FreezeBoss();
        // enemyData.isDeath = isDeath;
        // if(health.Value > 0)
        // {
        //     enemyData.isDeath = isDeath;
        // }
        // if(health.Value == 0)
        // {
        //     isDeath = enemyData.isDeath;
        // }
        // if(isDeath == true)
        // {
        //     isDeath = enemyData.isDeath;
        //     bossRB.bodyType = RigidbodyType2D.Static;
        //     bossCollider.enabled = false;
        //     sp.enabled = false;
        //     healthBarCanvas.enabled = false;
        // }
        if(SaveGameManager.Instance.DeathStates.Contains(uniqueID.ID))
        {
            isDeath = enemyData.isDeath;
            bossRB.bodyType = RigidbodyType2D.Static;
            bossCollider.enabled = false;
            sp.enabled = false;
            healthBarCanvas.enabled = false;
        }
        else
        {
            health.Value = 1;
        }

    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Die()
    {
        base.Die();
        SaveGameManager.Instance.DeathStates.Add(uniqueID.ID);
        healthBarCanvas.enabled = false;
        bossAnim.SetBool("IsDeath", true);
        enemyData.isDeath = true;
        isDeath = enemyData.isDeath;
        enableAct = false;
        bossRB.bodyType = RigidbodyType2D.Static;
        bossCollider.enabled = false;
        sp.enabled = false;

    }
    void FlipBoss()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x && isDeath == false)
        {
            rotation.y = 0;
        }
        else
        {
            rotation.y = 180;
        }
        transform.eulerAngles = rotation;
    }

    void MoveBoss()
    {
        if((target.position - transform.position).magnitude >= 5&& isDeath == false)
        {
            bossAnim.SetBool("CanWalk", true);
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        if((target.position - transform.position).magnitude < 5)
        {
            bossAnim.SetBool("CanWalk", false);
        }
    }

    private void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
            if (tPlayer != null)
            {
                target = tPlayer.transform;
            }
        }
        if(enableAct)
        {
            FlipBoss();
            MoveBoss();
        }
    }

    void BossAtk()
    {
        if((target.position - transform.position).magnitude < 5 && isDeath == false)
        {
            switch(atkStep)
            {
                case 0:
                    atkStep += 1;
                    bossAnim.Play("Attack1");
                    break;
                case 1:
                    atkStep += 1;
                    bossAnim.Play("Attack2");
                    break;
                case 2:
                    atkStep = 0;
                    bossAnim.Play("Attack3");
                    break;
            }
        }
    }
    void FreezeBoss()
    {
        enableAct = false;
    }
    void UnFreezeBoss()
    {
        enableAct = true;
    }

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");
        if(!Player.MyInstance.input.AttackInputs[(int)CombatInputs.fireElement])
        {
            PlayerSpecialEnergy.Instance.Obtain(0.1f);
        }
        TakeDamage(amount);
    }
}
