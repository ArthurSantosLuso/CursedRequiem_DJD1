using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Lifetime Settings")]
    [SerializeField] private float maxLifeTime = 5f;

    private Rigidbody2D rb;
    private Transform targetPlayer;
    private float lifeTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // No gravity so it can fly
    }

    private void Start()
    {
        lifeTimer = maxLifeTime;
    }

    private void Update()
    {
        HandleLifeTime();
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        targetPlayer = hit != null ? hit.transform : null;

    }

    private void MoveTowardsPlayer()
    {
        if (targetPlayer == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocityY);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void HandleLifeTime()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
