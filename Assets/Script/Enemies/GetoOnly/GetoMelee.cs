using UnityEngine;

public class GetoMelee : MonoBehaviour
{
    // This script handles the Geto's melee attack search and dealing damage, if found.

    [Header("Melee Attack Configuration")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int attackDamage;
    

    public void MeleeAttack()
    {
        // Look for game objects that are part of the specified layer and inside the attack range
        Collider2D target = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer);
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
