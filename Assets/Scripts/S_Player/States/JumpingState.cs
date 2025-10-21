using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    PlayerMovement player;
    bool isHoldingJump;
    public JumpingState(PlayerMovement player)
    {
        this.player = player;
    }
    public void UpdateState()
    {
        player.rb.velocity = new Vector2(player.inputX * player.moveSpeed, player.rb.velocity.y);
        ApplyBetterJump();

        // Mientras mantenga el botón y no haya pasado el tiempo máximo
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

        // Cuando suelta el botón o empieza a caer
        if (player.rb.velocity.y <= 0f)
        {
            player.machine.ChangeState(player.machine.fallingState);
        }
    }
    public void Enter()
    {
        Debug.Log("JUMPING");

        // Aplicar el impulso inicial
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
        player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);

        player.jumpTime = 0f;
        isHoldingJump = true;
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
