using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.25f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float turnCooldown = 0.15f;

    [SerializeField, Min(0f)] private float contactDamage = 20f;
    [SerializeField] private float damageCooldown = 1f;

    private float nextDamageTime;
    private float nextTurnTime;

    private void FixedUpdate()
    {
        if (IsDead || rb == null) return;

        Move();
    }

    private void Move()
    {
        Vector3 ls = transform.localScale;
        Vector2 rayDirection;

        if (isFacingRight)
        {
            rayDirection = Vector2.right;
            ls.x = Mathf.Abs(ls.x);
        }
        else
        {
            rayDirection = Vector2.left;
            ls.x = -Mathf.Abs(ls.x);
        }

        transform.localScale = ls;

        if (wallCheck != null && Time.time >= nextTurnTime)
        {
            if (Physics2D.Raycast(wallCheck.position, rayDirection, wallCheckDistance, groundLayer))
            {
                isFacingRight = !isFacingRight;
                nextTurnTime = Time.time + turnCooldown;
                return;
            }
        }

        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryDamagePlayer(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDamagePlayer(collision);
    }

    private void TryDamagePlayer(Collider2D targetCollider)
    {
        if (IsDead || Time.time < nextDamageTime) return;

        if (targetCollider.CompareTag("Player"))
        {
            Character playerCharacter = targetCollider.GetComponentInParent<Character>();
            if (playerCharacter != null && !playerCharacter.IsDead)
            {
                playerCharacter.TakeDamage(contactDamage);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    protected override void Die()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}