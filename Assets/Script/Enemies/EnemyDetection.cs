using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // This script should control the enemy's detection area. If the enemy with this script detects the player,
    // it will automatically change direction and chase the player to a point where it can attack, and then proceed to attack.

    [SerializeField]
    private Transform detectionPoint;
    [SerializeField]
    private float detectionRange = 1f;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
        }
    }
}
