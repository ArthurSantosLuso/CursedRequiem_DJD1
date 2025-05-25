using System.Runtime.CompilerServices;
using UnityEngine;

public class GetoRanged : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Ranged Attack Configuration")]
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int attackDamage;

    private bool isFollowing = true;

    public void RangedAttack()
    {
        // Look for game objects that are part of the specified layer and inside the attack range
        Collider2D target = Physics2D.OverlapBox(attackPoint.position, attackSize, 0, targetLayer);
        if (target != null)
        {
            // Check if the found game object is the player
            PlayerHealthScript playerHealth = target.GetComponent<PlayerHealthScript>();
            if (playerHealth != null)
            {
                // Damage players
                playerHealth.DamagePlayer(attackDamage);
            }
        }
    }

    private void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 nextPosition = transform.position;

        // Take players position
        nextPosition.x = player.position.x;

        // Make the portal follow the player
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, 2);
    }


    private void StopFollowing() => isFollowing = false;

    private void StartFollowing() => isFollowing = true;

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position, attackSize);
        }
    }
}
