using System;
using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    // Esse script deve tomar conta da logica de ataque do inimigo.
    // Cooldown do ataque, dano, range, delay do ataque, etc... são o que devem ser tratados aqui.

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

    private bool canAttack = true;
    private EnemyStateManager stateManager;

    private void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }

    private void Update()
    {
        Debug.Log(stateManager.CurrentState);

        // Se o inimigo foi afetado pela condição de sleep, não pode fazer nada.
        if (stateManager.IsSleeping())
            return;

        Collider2D target = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetLayer);

        if (target != null)
        {
            if (stateManager.CurrentState != EnemyStates.Attacking)
                stateManager.SetState(EnemyStates.Attacking);

            if (canAttack)
                StartCoroutine(Attack());
        }
        else if (stateManager.CurrentState == EnemyStates.Attacking)
        {
            stateManager.SetState(EnemyStates.Waiting);
        }
    }

    /// <summary>
    /// Trata de tocar a animação de ataque e do delay entre cada ataque.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        if (stateManager.IsSleeping())
            yield return null;

        canAttack = false;
        yield return new WaitForSeconds(attackDelay);

        animator.SetTrigger("attack");

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    /// <summary>
    /// Procura pelo jogador, e se achar, aplica o dano no mesmo.
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

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}

