    using System;
using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    // This script should handle the enemy's attack logic.
    // Attack cooldown, damage, range, attack delay, etc... should be managed here.

    [Header("Attack Configurations")]
    [SerializeField]
    private float attackCooldown = 1.5f;
    [SerializeField]
    private int attackDamage = 10;
    [SerializeField]
    private float attackDelay = 0.5f;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private Transform attackPoint;

    [Header("Other Configurations")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject slashFX;

    private bool canAttack = true;
    private EnemyStateManager stateManager;

    private Coroutine attackCoroutine;
    private Coroutine waitCoroutine;

    private void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }

    private void Update()
    {

        // If the enemy has been affected by the sleep condition, it can't do anything.
        if (stateManager.IsSleeping())
        {
            StopCurrentAttack();
            return;
        }            

        Collider2D target = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer);

        if (target != null)
        {
            ResetWaiting();

            // Verify if enemy is alredy attacking and if it can attack
            if (stateManager.CurrentState != EnemyStates.Attacking && canAttack && attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        else if (stateManager.CurrentState == EnemyStates.Attacking)
        {
            stateManager.SetState(EnemyStates.Waiting);
            waitCoroutine = StartCoroutine(WaitBeforeWalkAgain());

        }
    }

    /// <summary>
    /// Handles playing the attack animation and the delay between each attack.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        stateManager.SetState(EnemyStates.Attacking);

        canAttack = false;

        yield return new WaitForSeconds(attackDelay);

        // Check again before attacking, in case state changed during delay
        if (!stateManager.IsSleeping())
        {
            animator.SetTrigger("attack");
            slashFX.SetActive(false);
            slashFX.SetActive(true);
        }

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        attackCoroutine = null;
    }

    /// <summary>
    /// Searches for the player, and if found, applies damage.
    /// </summary>
    public void ApplyDamage()
    {
        Collider2D target = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer);
        if (target != null)
        {
            PlayerHealthScript playerHealth = target.GetComponent<PlayerHealthScript>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(attackDamage);
            }
        }
    }

    private void StopCurrentAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            canAttack = true;
        }
    }

    private void ResetWaiting()
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
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

    private IEnumerator WaitBeforeWalkAgain()
    {
        yield return new WaitForSeconds(1f);
        stateManager.SetState(EnemyStates.Patrolling);
        waitCoroutine = null;
    }
}
