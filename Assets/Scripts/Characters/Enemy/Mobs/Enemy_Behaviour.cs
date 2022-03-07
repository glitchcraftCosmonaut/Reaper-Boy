using UnityEngine;

public class Enemy_Behaviour : Character, IDamageable
{
    public EnemyBehaviourData enemyData;
    public Transform leftLimit;
    public Transform rightLimit;
    public GameObject hotZone;
    public GameObject triggerArea;
    public string attackAnimName;
    [HideInInspector] public Transform target;



    private Animator anim;
    protected float distance; //distance between enemy and player
    private bool attackMode;
    private bool cooling; // check if enemy is cooling after attack
    private float intTimer; // initial timer



    protected override void OnEnable()
    {
        base.OnEnable();
        SelectTarget();
        intTimer = enemyData.timer;
        anim = GetComponent<Animator>();
        sp = GetComponentInChildren<SpriteRenderer>();
        defaultMat2D = GetComponentInChildren<SpriteRenderer>().material;
    }

    private void Update()
    {
        if(!attackMode)
        {
            Move();
        }
        if(!InsideOfLimits() && !enemyData.inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName(attackAnimName))
        {
            SelectTarget();
        }

        if(enemyData.inRange)
        {
            EnemyLogic();
        }

    }
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > enemyData.attackDistance)
        {
            StopAttack();
        }
        else if(enemyData.attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if(cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("CanWalk",true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName(attackAnimName))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyData.moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        enemyData.timer = intTimer; //reset timer when player enter attack range
        attackMode = true; // to check if enemy can still attack or not
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        enemyData.timer -= Time.deltaTime;
        if(enemyData.timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            enemyData.timer = intTimer;
        }
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 0;
        }
        else
        {
            rotation.y = 180;
        }
        transform.eulerAngles = rotation;
    }


    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");
        TakeDamage(amount);
    }
}
