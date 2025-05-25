using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float damping = 5f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 6f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Lifetime Settings")]
    [SerializeField] private float selfDestructTime = 5f;

    [Header("Attack Settings")]
    [SerializeField] private int onCollisionnDamage = 10;

    private Rigidbody2D rb;
    private Transform targetPlayer;
    // private float lifeTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        targetPlayer = hit ? hit.transform : null;
    }

    private void HandleMovement()
    {
        if (targetPlayer == null)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, damping * Time.fixedDeltaTime);
            return;
        }

        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        Vector2 velocity = direction * maxSpeed;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, velocity, acceleration * Time.fixedDeltaTime);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealthScript playerHealth = other.GetComponent<PlayerHealthScript>();
        if (playerHealth != null)
        {
            playerHealth.DamagePlayer(onCollisionnDamage);
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
