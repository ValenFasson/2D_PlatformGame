using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    PlayerMovement player;
    public IdleState(PlayerMovement player)
    {
        this.player = player;
    }
    public void UpdateState()
    {
        if (player.inputX != 0) 
        { 
            // entra en running
            player.machine.ChangeState(player.machine.runningState);
        }
        if (Input.GetButtonDown("Jump")) 
        {
            //entra al jump state
            player.machine.ChangeState(player.machine.jumpingState);
        }
    }
    public void Enter()
    {
        Debug.Log("IDLE");
    }
}
