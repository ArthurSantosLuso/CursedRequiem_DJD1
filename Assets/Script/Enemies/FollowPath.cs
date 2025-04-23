using System.Runtime.InteropServices;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Esse script deve tomar conta da l�gica de patrulha do inimigo.
    // Essa l�gica de "waypoints" funciona, mas n�o gosto muito dela. Se poss�vel, alterar depois para algo mais sofisticado.

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
        // Se o inimigo foi afetado pela condi��o de sleep, n�o pode fazer nada.
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
    /// Faz o inimigo mover-se em dire��o do proximo ponto.
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
    /// Trata do flip do inimigo de acordo com a dire��o que est� a se mover.
    /// </summary>
    private void Flip()
    {
        if (direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }


}




