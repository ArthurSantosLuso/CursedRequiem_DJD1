using System.Runtime.InteropServices;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // This script should handle the enemy patrol logic.
    // This "waypoints" logic works, but I don't like it much. If possible, change it later to something more sophisticated.

    [SerializeField]
    private UnityEngine.Transform[] waypoints;
    [SerializeField]
    private float speed = 2f;

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
            direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        // If the enemy has been affected by the sleep condition, it can't do anything.
        if (stateManager.IsSleeping())
            return;

        if (waypoints.Length == 0 || stateManager.CurrentState != EnemyStates.Patrolling)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveAlongPath();
    }

    /// <summary>
    /// Makes the enemy move toward the next point.
    /// </summary>
    private void MoveAlongPath()
    {
        if (stateManager.IsSleeping())
            return;

        UnityEngine.Transform targetWaypoint = waypoints[currentWaypointIndex];
        direction = (targetWaypoint.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
        anim.SetFloat("VelocityX", rb.linearVelocityX);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 3.0f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            Flip();
        }
    }

    /// <summary>
    /// Handles flipping the enemy according to the direction it's moving.
    /// </summary>
    private void Flip()
    {
        if (direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
