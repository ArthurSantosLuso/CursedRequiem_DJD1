using System.Collections;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    // Essa classe toma conta dos estados do inimigo. Deve sempre ser referenciada em
    // todos os tipos de inimigos para controlar o que cada inimigo faz e fazer os scripts funcionarem corretamente.

    public EnemyStates CurrentState { get; private set; } = EnemyStates.Patrolling;
    
    private Coroutine waitCoroutine;

    /// <summary>
    /// Muda o estado do inimigo.
    /// </summary>
    /// <param name="newState">Novo estado do inimigo</param>
    public void SetState(EnemyStates newState)
    {
        // Se o novo estado for o atual, não fazer nada.
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        if (newState == EnemyStates.Waiting)
        {
            if (waitCoroutine != null)
                StopCoroutine(waitCoroutine);
            waitCoroutine = StartCoroutine(WaitBeforePatrolling());
        }
    }

    private IEnumerator WaitBeforePatrolling()
    {
        yield return new WaitForSeconds(2f);
        SetState(EnemyStates.Patrolling);
    }

    /// <summary>
    /// Função para verificar se o inimigo está a dormir.
    /// </summary>
    /// <returns>Se o inimigo está a dormir.</returns>
    public bool IsSleeping() => CurrentState == EnemyStates.Sleeping;
}
