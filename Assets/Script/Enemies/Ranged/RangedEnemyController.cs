using OkapiKit;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float shootingRadius = 6f;
    [SerializeField] private float avoidanceRadius = 2.5f;
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private RangedEnemyShooter shooter;
    [SerializeField] private EnemyAvoidanceMovement avoidance;
    [SerializeField] private MonoBehaviour patrolScript; // Reference to your patrol script

    private Transform player;

    
    private void Update()
    {
        DetectPlayer();    
    }

    private void DetectPlayer()
    {
        Collider2D closeRange = Physics2D.OverlapCircle(transform.position, avoidanceRadius, playerLayer);
        Collider2D inRange = Physics2D.OverlapCircle(transform.position, shootingRadius, playerLayer);

        if (closeRange)
        {
            player = closeRange.transform;
            avoidance.StartAvoiding(player);
            shooter.StopShooting();
            patrolScript.enabled = false;
        }
        else if (inRange)
        {
            player = inRange.transform;
            avoidance.StopAvoiding();
            shooter.StartShooting(player);
            patrolScript.enabled = false;
        }
        else
        {
            player = null;
            shooter.StopShooting();
            avoidance.StopAvoiding();
            patrolScript.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
}
