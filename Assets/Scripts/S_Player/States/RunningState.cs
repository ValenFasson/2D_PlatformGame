using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState
{
    PlayerMovement player;
    public RunningState(PlayerMovement player)
    {
        this.player = player;
    }
    public void UpdateState()
    {
        player.rb.velocity = new Vector2(player.inputX * player.moveSpeed, player.rb.velocity.y);
        if (player.inputX == 0)
        {
            // entra en Idle
            player.machine.ChangeState(player.machine.idleState);
        }
        if (Input.GetButtonDown("Jump"))
        {
            // jumping state
            player.machine.ChangeState(player.machine.jumpingState);
        }
    }
    public void Enter()
    {
        Debug.Log("RUNNING");
    }
}
