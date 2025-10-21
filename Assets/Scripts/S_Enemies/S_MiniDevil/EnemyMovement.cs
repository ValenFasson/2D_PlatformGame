using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private IEnemy enemyFacadeUser;
    [SerializeField] private Transform[] MovementDots;
    [SerializeField] private float speed;
    private int nextDot = 1;
    [SerializeField] private bool direction = true; // T = + F = -
    [SerializeField] public bool isLooping = false;

    private void Update()
    {
        if (enemyFacadeUser == null) enemyFacadeUser = GetComponent<IEnemy>();
        if (isLooping) 
        {
            direction = true;
            Loop();
        }
        else { PointToPoint();}
       transform.position = Vector2.MoveTowards(transform.position, MovementDots[nextDot].position, speed * Time.deltaTime);
    }

    private void Loop() 
    {
        if (direction && nextDot >= MovementDots.Length) 
        {
            nextDot = 0;
        }

        if (Vector2.Distance(transform.position, MovementDots[nextDot].position) < 0.1f)
        {
            nextDot += 1;
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
            // Notify facade that enemy is patrolling (moved to next point)
            enemyFacadeUser?.TriggerFacade("Patrolling!");
        }
    }
}
