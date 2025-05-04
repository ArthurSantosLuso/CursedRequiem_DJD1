using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawnerController : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float avoidanceRadius = 2f;
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private EnemyAvoidanceMovement avoidance;

    private Transform player;

    public bool IsPlayerInSpawnArea { get; private set; }
    public bool IsAvoidingPlayer { get; private set; }
    public Transform PlayerReference { get; private set; }


    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider2D detection = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        Collider2D avoidanceCheck = Physics2D.OverlapCircle(transform.position, avoidanceRadius, playerLayer);

        if (detection)
        {
            player = detection.transform;
            spawner.StartSpawning();

            IsPlayerInSpawnArea = true;
            PlayerReference = player;

            if (avoidanceCheck)
            {
                avoidance.StartAvoiding(player);
                IsAvoidingPlayer = true;
            }
            else
            {
                avoidance.StopAvoiding();
                IsAvoidingPlayer = false;
            }
        }
        else
        {
            player = null;
            PlayerReference = null;
            IsPlayerInSpawnArea = false;
            IsAvoidingPlayer = false;

            spawner.StopSpawning();
            avoidance.StopAvoiding();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}
