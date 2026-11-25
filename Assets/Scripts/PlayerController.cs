using System.Collections;
using UnityEngine;

public class PlayerController : Character
{
    private float horizontal;
    private float speed = 8f;

    private float jumpPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public static PlayerController Instance;

    public int coinNumber = 0;

    /////////

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] private TrailRenderer tr;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;

        Move();

        Jump();

        Flip();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f ||
            !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void AddCoin()
    {
        coinNumber++;
        UIManager.Instance.UpdateText(coinNumber);

        if (coinNumber == 10)
        {
            Debug.Log("You Win!!");
        }
    }

    public IEnumerator Dash()
    {
        // YA le he dado al dash. Por lo que no puedo dashear otra vez.
        canDash = false;
        isDashing = true;

        // Guardar la gravedad original y settear la del rb a 0
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        // Cambiamos la velocidad    DIRECCION                FUERZA    Eje Y
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        // Abrir el trail
        tr.emitting = true;

        // Duración del dash
        yield return new WaitForSeconds(dashTime);

        // Termina el dash
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        // Cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    protected override void Die()
    {
        Debug.Log("Player se ha muerto");
    }
}
