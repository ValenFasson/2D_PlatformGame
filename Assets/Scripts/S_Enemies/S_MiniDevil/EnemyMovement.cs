using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private SO_DevGho Data; //ScriptableObject
    [SerializeField] private Transform[] MovementDots;
    private int nextDot = 1;
    [SerializeField] private bool direction = true; // T = + F = -
    [SerializeField] public bool isLooping = false;

    private void Update()
    {
        if (MovementDots == null || MovementDots.Length == 0) return;
        if (nextDot < 0 || nextDot >= MovementDots.Length) nextDot = 0;

        if (isLooping)
        {
            direction = true;
            Loop();
        }
        else
        {
            PointToPoint();
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            MovementDots[nextDot].position,
            Data.speed * Time.deltaTime
        );
    }

    private void Loop() 
    {
        if (Vector2.Distance(transform.position, MovementDots[nextDot].position) < 0.1f)
        {
            nextDot++;
            if (nextDot >= MovementDots.Length)
            { 
                nextDot = 0; 
            }
        }
    }

    private void PointToPoint() 
    {
        if (direction && nextDot + 1 >= MovementDots.Length)
        {
            direction = false;
        }
        if (!direction && nextDot <= 0)
        {
            direction = true;
        }

        if (Vector2.Distance(transform.position, MovementDots[nextDot].position) < 0.1f)
        {
            if (direction) { nextDot += 1; }
            else { nextDot -= 1; }
        }
    }
}
