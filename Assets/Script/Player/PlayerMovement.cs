using System.Collections;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // This script handles the player's movement logic.
    // Horizontal movement (left & right), vertical movement (jump),
    // flip, etc... are examples of things this script should manage.

    // This script still needs significant improvement.

    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private string horizontalAxisName = "Horizontal";
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius = 2.0f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float jumpMaxDuration;
    [SerializeField]
    private float jumpGravity;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private float jumpTimer;
    private float originalGravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalGravity = rb.gravityScale;
        animator = GetComponent<Animator>();

        // GetComponent<CapsuleCollider2D>().enabled = true;
        // GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        CheckIsGrounded();

        float moveDir = Input.GetAxis(horizontalAxisName);

        Vector2 currentVelocity = rb.linearVelocity;

        currentVelocity.x = moveDir * velocity.x;

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                currentVelocity.y = velocity.y;
                jumpTimer = 0f;
                rb.gravityScale = jumpGravity;
            }
        }
        else if (jumpTimer < jumpMaxDuration)
        {
            jumpTimer = jumpTimer + Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                rb.gravityScale = jumpGravity;
            }
            else
            {
                jumpTimer = jumpMaxDuration;
                rb.gravityScale = originalGravity;
            }
        }
        else
        {
            rb.gravityScale = originalGravity;
        }

        rb.linearVelocity = currentVelocity;

        if (moveDir < 0)
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        else if (moveDir > 0)
            transform.rotation = Quaternion.identity;

        // GetComponent<CapsuleCollider2D>().enabled = isGrounded;
        // GetComponent<BoxCollider2D>().enabled = !isGrounded;

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
        animator.SetFloat("VelocityY", currentVelocity.y);
    }

    /// <summary>
    /// Checks if the player is touching the ground or not.
    /// </summary>
    void CheckIsGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
