using UnityEngine;


public class StateMachine : MonoBehaviour
{
    public IdleState idleState;
    public RunningState runningState;
    public AirborneState airborneState; // unified jump+fall
    IState currentState;

    public PlayerMovement player;
    public Rigidbody2D rb;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>(); // safer than reading player.rb in Awake order

        idleState = new IdleState(player);
        runningState = new RunningState(player);
        airborneState = new AirborneState(player);
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
        Debug.Log($"? Entering State: {newState.GetType().Name}");
        currentState.Enter();
    }
}