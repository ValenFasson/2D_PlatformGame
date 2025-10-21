using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMovement : MonoBehaviour
{
    [SerializeField] private SO_Reaper Data; // ScriptableObject
    [SerializeField] public Transform player;
    [SerializeField] public bool canTurnBack;
    private Vector2 StartPosition;
    private float ActualDistance;

    public void Awake()
    {
        StartPosition = transform.position;
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if(playerGO != null) 
        {
            player = playerGO.transform;
        }
    }
    public void Update()
    {
        ActualDistance = Vector2.Distance(player.position, transform.position); 
        
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
        if (ActualDistance < Data.ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Data.speed * Time.deltaTime);
        }
    }
    public void FollowingNBack() 
    {
        if (ActualDistance < Data.ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Data.speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, StartPosition, Data.speed * Time.deltaTime);
        }
    }
}
