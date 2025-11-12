using UnityEngine;

public class RunningState : IState
{
    PlayerMovement player;

    public RunningState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Debug.Log("RUNNING");
    }

    public void UpdateState()
    {
        player.rb.velocity = new Vector2(player.inputX * player.moveSpeed, player.rb.velocity.y);

        if (Mathf.Abs(player.inputX) <= 0.01f)
        {
            player.machine.ChangeState(player.machine.idleState);
            return;
        }

        if (Input.GetButtonDown("Jump") && player.IsGrounded())
        {
            player.machine.ChangeState(player.machine.airborneState);
            return;
        }
    }
}