using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;

    [Header("Salto")]
    public float jumpForce = 9f;             // impulso inicial (puede ser mas bajo que antes)
    public float maxJumpHoldTime = 0.25f;    // tope de tiempo para "cargar" el salto
    public float jumpHoldForce = 22f;        // fuerza continua mientras se mantiene ESPACIOmy

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
        
        rb = GetComponent<Rigidbody2D>();
        baseScaleX = Mathf.Abs(transform.localScale.x) > 0.001f ? Mathf.Abs(transform.localScale.x) : 1f;
        
    }

    private void Start()
    {
        machine.Initialize();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        // cortar la carga si suelta el bot�n
        HandleFlip();

        machine.UpdateState();
        // inicio de salto
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

    public bool IsGrounded()
    {
        if (groundCheck == null)
            return false;

        return Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
