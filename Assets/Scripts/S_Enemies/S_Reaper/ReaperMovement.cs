using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMovement : MonoBehaviour
{
    [SerializeField] public float ThresholdDistance = 200f;
    [SerializeField] public Transform player;
    [SerializeField] public float speed;
    [SerializeField] public bool canTurnBack;
    private Vector2 StartPosition;
    private float ActualDistance;
    private IEnemy enemyFacadeUser;

    public void Awake()
    {
        StartPosition = transform.position;
    }
    public void Update()
    {
        if (enemyFacadeUser == null) enemyFacadeUser = GetComponent<IEnemy>();
        ActualDistance = Vector2.Distance(player.transform.position, transform.position); 
        
        if (!canTurnBack) 
        {
            Following();
        }
        else 
        {
            FollowingNBack();
        }

    }
    public void Following() 
    {
        if (ActualDistance < ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            // Notify facade: staying put (chasing)
            enemyFacadeUser?.TriggerFacade("Staying put!");
        }
    }
    public void FollowingNBack() 
    {
        if (ActualDistance < ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            // Notify facade: staying put (chasing)
            enemyFacadeUser?.TriggerFacade("Staying put!");
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, StartPosition, speed * Time.deltaTime);
            // Notify facade: returning to start
            enemyFacadeUser?.TriggerFacade("Returning!");
        }
    }
}
