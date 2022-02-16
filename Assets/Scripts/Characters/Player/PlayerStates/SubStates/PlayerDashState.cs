using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{

    public bool CanDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 lastAIPos;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerDashData playerDashData, string animBoolName) : base(player, stateMachine, playerDashData, animBoolName)
    {
        // hasDash = playerDashData.isHasDash;
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.input.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * core.Movement.FacingDirection;
        // Time.timeScale = playerDashData.holdTimeScale;
        // startTime = Time.unscaledTime;

        // player.DashDirectionIndicator.gameObject.SetActive(true);

    }

    public override void Exit()
    {
        base.Exit();

        if(core.Movement.CurrentVelocity.y > 0)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerDashData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {

            player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));


            if (isHolding)
            {
                // dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.input.DashInputStop;

                // if(dashDirectionInput != Vector2.zero)
                // {
                //     dashDirection = dashDirectionInput;
                //     dashDirection.Normalize();
                // }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                // player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if(dashInputStop || Time.unscaledTime >= startTime + playerDashData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerDashData.drag;
                    core.Movement.SetVelocity(playerDashData.dashVelocity, dashDirection);
                    // player.DashDirectionIndicator.gameObject.SetActive(false);
                    // PlaceAfterImage();
                }
            }
            else
            {
                core.Movement.SetVelocity(playerDashData.dashVelocity, dashDirection);
                // CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + playerDashData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    // private void CheckIfShouldPlaceAfterImage()
    // {
    //     if(Vector2.Distance(player.transform.position, lastAIPos) >= playerDashData.distBetweenAfterImages)
    //     {
    //         PlaceAfterImage();
    //     }
    // }

    // private void PlaceAfterImage()
    // {
    //     PlayerAfterImagePool.Instance.GetFromPool();
    //     lastAIPos = player.transform.position;
    // }

    public bool CheckIfCanDash()
    {
        return CanDash && playerDashData.hasDash && Time.time >= lastDashTime + playerDashData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}
