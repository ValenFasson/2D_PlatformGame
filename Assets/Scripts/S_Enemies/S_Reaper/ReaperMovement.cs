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

    public void Awake()
    {
        StartPosition = transform.position;
    }
    public void Update()
    {
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
        }
    }
    public void FollowingNBack() 
    {
        if (ActualDistance < ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, StartPosition, speed * Time.deltaTime);
        }
    }
}
