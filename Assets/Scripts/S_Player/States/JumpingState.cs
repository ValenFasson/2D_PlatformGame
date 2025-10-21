using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    PlayerMovement player;
    public JumpingState(PlayerMovement player)
    {
        this.player = player;
    }
    public void UpdateState()
    {
        player.rb.velocity = new Vector2(player.inputX * player.moveSpeed, player.rb.velocity.y);

        while (Input.GetButton("Jump") && !IsGrounded()) 
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
            player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
            player.jumpTime = 0f;

            if (!Input.GetButtonUp("Jump")) 
            {
                player.jumpTime += Time.fixedDeltaTime;
                if (player.jumpTime < player.maxJumpHoldTime && player.rb.velocity.y >= 0f)
                {
                    player.rb.AddForce(Vector2.up * player.jumpHoldForce * Time.fixedDeltaTime, ForceMode2D.Force);
                }
            }
            else
            {
                ApplyBetterJump();
                // Falling
                player.machine.ChangeState(player.machine.fallingState);
            }
        }
            //running
            player.machine.ChangeState(player.machine.runningState);
    }
    public void Enter()
    {
        Debug.Log("JUMPING");
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(player.groundCheck.position, Vector2.down, player.groundCheckDistance, player.groundLayer);
    }

    void ApplyBetterJump()
    {
        if (player.rb.velocity.y < 0f)
        {
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else
        {
            if (player.rb.velocity.y > 0f && !Input.GetButton("Jump"))
            {
                player.rb.velocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }
        }
    }
}
