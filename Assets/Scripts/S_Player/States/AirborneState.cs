
using UnityEngine;

public class AirborneState : IState
{
    PlayerMovement player;
    bool isHoldingJump;
    float groundedGraceTimer;

    public AirborneState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Debug.Log("AIRBORNE");
        // reset vertical then apply impulse
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
        player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);

        player.jumpTime = 0f;
        isHoldingJump = true;
        groundedGraceTimer = 0.1f;
    }

    public void UpdateState()
    {
        // horizontal air control
        player.rb.velocity = new Vector2(player.inputX * player.moveSpeed, player.rb.velocity.y);
        ApplyBetterJump();

        if (groundedGraceTimer > 0f)
            groundedGraceTimer -= Time.deltaTime;

        // variable jump height (hold)
        if (isHoldingJump && Input.GetButton("Jump"))
        {
            player.jumpTime += Time.fixedDeltaTime;

            if (player.jumpTime < player.maxJumpHoldTime && player.rb.velocity.y > 0f)
            {
                player.rb.AddForce(Vector2.up * player.jumpHoldForce * Time.fixedDeltaTime, ForceMode2D.Force);
            }
            else
            {
                isHoldingJump = false;
            }
        }

        // Land transitions
        if (groundedGraceTimer <= 0f && player.IsGrounded())
        {
            if (Mathf.Abs(player.inputX) > 0.01f)
                player.machine.ChangeState(player.machine.runningState);
            else
                player.machine.ChangeState(player.machine.idleState);
        }
    }

    void ApplyBetterJump()
    {
        if (player.rb.velocity.y < 0f)
        {
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else if (player.rb.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}