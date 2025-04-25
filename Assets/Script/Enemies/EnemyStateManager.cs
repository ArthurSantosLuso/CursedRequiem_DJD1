using System.Collections;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    // This class manages the enemy's states. It should always be referenced
    // in all types of enemies to control what each enemy does and make scripts work properly.

    public EnemyStates CurrentState { get; private set; } = EnemyStates.Patrolling;

    private Coroutine waitCoroutine;

    /// <summary>
    /// Changes the enemy's state.
    /// </summary>
    /// <param name="newState">New state of the enemy</param>
    public void SetState(EnemyStates newState)
    {
        // If the new state is the same as the current one, do nothing.
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
    /// Function to check if the enemy is sleeping.
    /// </summary>
    /// <returns>Whether the enemy is sleeping.</returns>
    public bool IsSleeping() => CurrentState == EnemyStates.Sleeping;
}
