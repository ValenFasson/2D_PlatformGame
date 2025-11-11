using UnityEngine;

public class IdleState : IState
{
    PlayerMovement player;

    public IdleState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("IDLE");
    }

    public void UpdateState()
    {
        // horizontal move while idle (keeps velocity.x responsive if you like)
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);

        if (Mathf.Abs(player.inputX) > 0.01f)
        {
            player.machine.ChangeState(player.machine.runningState);
            return;
        }

        // Only jump if grounded to avoid lost impulse mid-airmy
        if (Input.GetButtonDown("Jump") && player.IsGrounded())
        {
            player.machine.ChangeState(player.machine.airborneState);
            return;
        }
    }
}