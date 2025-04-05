using System.Collections;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyStates CurrentState { get; private set; } = EnemyStates.Patrolling;
    private Coroutine waitCoroutine;

    public void SetState(EnemyStates newState)
    {
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
}
