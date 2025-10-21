using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IState
{
    PlayerMovement player;
    public FallingState(PlayerMovement player) 
    {
        this.player = player;
    }
    public void UpdateState()
    {
        player.rb.velocity = new Vector2(player.inputX * player.moveSpeed, player.rb.velocity.y);
        ApplyBetterJump();
        if (IsGrounded()) 
        {
            //Idle
            player.machine.ChangeState(player.machine.idleState);
        }
        if (IsGrounded() && player.inputX != 0) 
        {
            //running
            player.machine.ChangeState(player.machine.runningState);
        }
    }
    public void Enter()
    {
        Debug.Log("FALLING");
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
