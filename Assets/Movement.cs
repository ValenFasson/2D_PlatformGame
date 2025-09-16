using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;

    [Header("Salto")]
    public float jumpForce = 9f;             // impulso inicial (puede ser más bajo que antes)
    public float maxJumpHoldTime = 0.25f;    // tope de tiempo para "cargar" el salto
    public float jumpHoldForce = 22f;        // fuerza continua mientras se mantiene ESPACIO

    [Header("Gravedad")]
    public float gravityScale = 3f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    public PlayerState State { get; private set; }

    Rigidbody2D rb;
    float inputX;
    bool wantJump;
    float baseScaleX = 1f;

    // control del salto mantenido
    bool isJumping;
    float jumpTime;

    // Singleton del jugador (persistente entre escenas)
    static Movement instance;

    void Awake()
    {
        // Garantiza una única instancia del jugador
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        baseScaleX = Mathf.Abs(transform.localScale.x) > 0.001f ? Mathf.Abs(transform.localScale.x) : 1f;
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            wantJump = true;
        }

        // cortar la carga si suelta el botón
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        UpdateState();
        HandleFlip();
    }

    void FixedUpdate()
    {
        // movimiento horizontal
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        // inicio de salto
        if (wantJump && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isJumping = true;
            jumpTime = 0f;
        }
        wantJump = false;

        // mantener salto mientras se presiona (hasta el tope)
        if (isJumping && Input.GetButton("Jump"))
        {
            jumpTime += Time.fixedDeltaTime;

            // aplica fuerza hacia arriba mientras no se alcance el tope y el jugador siga subiendo
            if (jumpTime < maxJumpHoldTime && rb.velocity.y >= 0f)
            {
                rb.AddForce(Vector2.up * jumpHoldForce * Time.fixedDeltaTime, ForceMode2D.Force);
            }
            else
            {
                isJumping = false;
            }
        }

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

        // reset al tocar suelo
        if (IsGrounded())
        {
            isJumping = false;
        }
    }

    void UpdateState()
    {
        bool grounded = IsGrounded();
        float vy = rb.velocity.y;
        float ax = Mathf.Abs(rb.velocity.x);

        if (grounded)
        {
            if (ax > 0.05f)
            {
                State = PlayerState.Running;
            }
            else
            {
                State = PlayerState.Idle;
            }
        }
        else
        {
            if (vy > 0.1f)
            {
                State = PlayerState.Jumping;
            }
            else
            {
                State = PlayerState.Falling;
            }
        }
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
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