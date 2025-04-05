using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;
    [SerializeField]
    private float coyoteTime = 0.15f;

    [SerializeField]
    private Collider2D groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Collider2D groundCollider;
    [SerializeField]
    private Collider2D airCollider;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isTouchingGround;
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isTouchingGround)
            Debug.Log("Estou a tocar o chão");
        else Debug.Log("Não estou a tocar o chão");
    }


    void FixedUpdate()
    {
        // Horizontal Movement X
        float deltaX = Input.GetAxis("Horizontal") * speed.x;
        rb.linearVelocity = new Vector2(deltaX, rb.linearVelocityY);

        // Apply Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsGrounded();
            Jump();
        }

        // Air Control
        if (!isTouchingGround)
        {
            rb.linearVelocity += new Vector2(deltaX * speed.x * 0.1f, 0);
        }

        // Collider Swap
        groundCollider.enabled = isTouchingGround;
        airCollider.enabled = !isTouchingGround;

        // Animator Parameters
        animator.SetFloat("AbsVelocityX", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("AbsVelocityY", Mathf.Abs(rb.linearVelocityY));
        animator.SetFloat("VelocityX", rb.linearVelocityX);
        animator.SetFloat("VelocityY", rb.linearVelocityY);
        animator.SetBool("IsGrounded", isTouchingGround);
    }

    private void IsGrounded()
    {
        if (groundCheck)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useLayerMask = true;
            contactFilter.layerMask = groundLayer;

            Collider2D[] results = new Collider2D[128];

            int n = Physics2D.OverlapCollider(groundCheck, contactFilter, results);
            if (n > 0)
            {
                isTouchingGround = true;
                //lastGroundTime = Time.time;
                return;
            }
            else
            {
                isTouchingGround = false;

            }
        }
    }

    private void Jump()
    {
        if (isTouchingGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, speed.y);
        }
    }
}