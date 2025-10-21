using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;

    [Header("Salto")]
    public float jumpForce = 9f;             // impulso inicial (puede ser m�s bajo que antes)
    public float maxJumpHoldTime = 0.25f;    // tope de tiempo para "cargar" el salto
    public float jumpHoldForce = 22f;        // fuerza continua mientras se mantiene ESPACIO

    [Header("Gravedad")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    public StateMachine machine;
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public float inputX;
    float baseScaleX = 1f;

    // control del salto mantenido
    public bool isJumping;
    public float jumpTime;

    // Singleton del jugador (persistente entre escenas)
    static PlayerMovement instance;
    void Awake()
    {
        machine = GetComponent<StateMachine>();
        // Garantiza una unica instancia del jugador
        rb = GetComponent<Rigidbody2D>();
        baseScaleX = Mathf.Abs(transform.localScale.x) > 0.001f ? Mathf.Abs(transform.localScale.x) : 1f;
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        // cortar la carga si suelta el bot�n
        HandleFlip();
    }

    void FixedUpdate()
    {
        machine.UpdateState();
        // inicio de salto

        // extra gravedad para mejor "feel"
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else
        {
            if (rb.velocity.y > 0f && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }
        }
    }

    void HandleFlip()
    {
        if (Mathf.Abs(inputX) > 0.02f)
        {
            float dir = Mathf.Sign(inputX);
            var s = transform.localScale;
            transform.localScale = new Vector3(baseScaleX * dir, s.y, s.z);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}