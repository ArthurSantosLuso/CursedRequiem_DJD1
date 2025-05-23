using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GetoPositionHandle : MonoBehaviour
{
    [SerializeField] private GetoStateManager stateManager;

    [Header("Positions Configuration")]
    [SerializeField] private Transform meleePosition;
    [SerializeField] private float timeStayMelee;
    [SerializeField] private Transform rangedPosition;
    [SerializeField] private float timeStayRanged;

    private Coroutine stateTimer;

    private void OnEnable()
    {
        stateManager.OnAnimationComplete += TeleportBossToStatePosition;
    }

    private void OnDisable()
    {
        stateManager.OnAnimationComplete -= TeleportBossToStatePosition;
    }

    private void TeleportBossToStatePosition()
    {
        Transform target = null;

        switch (stateManager.CurrentState)
        {
            case GetoStates.Melee:
                target = meleePosition;
                break;
            case GetoStates.Ranged:
                target = rangedPosition;
                break;
        }

        if (target != null)
        {
            transform.position = target.position;
            Debug.Log("Boss teleportado para: " + target.name);

            if (stateTimer != null)
                StopCoroutine(stateTimer);

            stateTimer = StartCoroutine(StateTimerRoutine());
        }
    }

    private IEnumerator StateTimerRoutine()
    {
        float waitTime = 0f;

        switch (stateManager.CurrentState)
        {
            case GetoStates.Melee:
                waitTime = timeStayMelee;
                break;
            case GetoStates.Ranged:
                waitTime = timeStayRanged;
                break;
        }

        yield return new WaitForSeconds(waitTime);

        if (stateManager.CurrentState == GetoStates.Melee)
            stateManager.SetState(GetoStates.Ranged);
        else
            stateManager.SetState(GetoStates.Melee);
    }
}
