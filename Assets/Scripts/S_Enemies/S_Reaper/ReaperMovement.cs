using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMovement : MonoBehaviour
{
    [SerializeField] public float ThresholdDistance = 200f;
    [SerializeField] public float speed;
    [SerializeField] public bool canTurnBack;

    [SerializeField] string playerTag = "Player"; // tag del jugador

    private Transform player;          // referencia encontrada por tag
    private Vector2 StartPosition;
    private float ActualDistance;

    public void Awake()
    {
        StartPosition = transform.position;
        FindPlayer();
    }

    public void Update()
    {
        // Si no hay referencia (p. ej. al cargar escena), intentar encontrarla
        if (player == null)
        {
            FindPlayer();
            if (player == null) return;
        }

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

    void FindPlayer()
    {
        GameObject go = GameObject.FindGameObjectWithTag(playerTag);
        if (go != null)
        {
            player = go.transform;
        }
    }

    public void Following()
    {
        if (ActualDistance < ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void FollowingNBack()
    {
        if (ActualDistance < ThresholdDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, StartPosition, speed * Time.deltaTime);
        }
    }
}