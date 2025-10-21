using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IdleState idleState;
    public RunningState runningState;
    public JumpingState jumpingState;
    public FallingState fallingState;
    IState currentState;
    public PlayerMovement player;
    public Rigidbody2D rb;

    public void Awake()
    {
        idleState = new IdleState(player);
        runningState = new RunningState(player);
        jumpingState = new JumpingState(player);  
        fallingState = new FallingState(player);
    }

    public void Initialize()
    {
        currentState = idleState;
        currentState.Enter();
    }
    public void UpdateState()
    {
        currentState.UpdateState();
    }
    public void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }
}
