using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float waitTime = 1.5f;

    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private Vector2 direction;
    private EnemyStateManager stateManager;
    private Animator anim;

    void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (waypoints.Length > 0)
            SetDirectionToCurrentWaypoint();
    }

    void FixedUpdate()
    {
        if (stateManager.IsSleeping())
            return;

        if (stateManager.CurrentState == EnemyStates.Attacking)
        {
            StopMovement();
            return;
        }

        if (waypoints.Length == 0 || stateManager.CurrentState != EnemyStates.Patrolling)
        {
            StopMovement();
            return;
        }

        HandlePatrol();
    }

    /// <summary>
    /// Controls movement and animation while patrolling between waypoints.
    /// </summary>
    private void HandlePatrol()
    {
        // Get the current target waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Calculate direction toward the waypoint
        direction = (targetWaypoint.position - transform.position).normalized;

        ApplyMovement();

        // Check if the enemy is close enough to pause and switch direction
        if (IsNearWaypoint(targetWaypoint))
        {
            StartCoroutine(PauseBeforeNextWaypoint());
        }

        UpdateAnimation();
    }

    /// <summary>
    /// Applies horizontal movement based on current direction and state.
    /// </summary>
    private void ApplyMovement()
    {
        // Use the current velocity to modify the horizontal component
        Vector2 velocity = rb.linearVelocity;
        velocity.x = (stateManager.CurrentState == EnemyStates.Waiting) ? 0 : direction.x * speed;

        rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocityY);

        if (stateManager.CurrentState != EnemyStates.Waiting)
            Flip();
    }

    /// <summary>
    /// Stops the enemy's movement completely.
    /// </summary>
    private void StopMovement() => rb.linearVelocity = Vector2.zero;

    /// <summary>
    /// Checks if the enemy is close enough to a waypoint.
    /// </summary>
    /// <param name="waypoint">The waypoint to check distance to.</param>
    /// <returns>True if within threshold distance.</returns>
    private bool IsNearWaypoint(Transform waypoint) =>
        Vector2.Distance(transform.position, waypoint.position) < 3.0f;

    /// <summary>
    /// Updates animation parameters based on current velocity.
    /// </summary>
    private void UpdateAnimation() =>
        anim.SetFloat("AbsVelocityX", Mathf.Abs(rb.linearVelocity.x) / speed);

    /// <summary>
    /// Sets the current direction toward the active waypoint.
    /// </summary>
    private void SetDirectionToCurrentWaypoint() =>
        direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

    /// <summary>
    /// Handles flipping the enemy according to the direction it's moving.
    /// </summary>
    private void Flip()
    {
        // If direction.x is positive, face right (0° rotation), else face left (180° rotation)
        float yRotation;
        if (direction.x >= 0)
            yRotation = 0f;
        else
            yRotation = 180f;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }


    /// <summary>
    /// Called when the enemy reaches a waypoint and needs to wait before moving again.
    /// </summary>
    /// <returns>Coroutine that waits before resuming patrol</returns>
    private IEnumerator PauseBeforeNextWaypoint()
    {
        stateManager.SetState(EnemyStates.Waiting);

        // Wait for the specified seconds
        yield return new WaitForSeconds(waitTime);

        // Move to next waypoint in sequence
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        SetDirectionToCurrentWaypoint();
        //Flip();

        stateManager.SetState(EnemyStates.Patrolling);
    }
}
