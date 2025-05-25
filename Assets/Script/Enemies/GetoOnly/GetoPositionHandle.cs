using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GetoPositionHandle : MonoBehaviour
{
    // This script handles Geto's position when ranged or melee.

    [SerializeField] private GetoStateManager stateManager;

    [Header("Positions Configuration")]
    [SerializeField] private Transform meleePosition;
    [SerializeField] private float timeStayMelee;
    [SerializeField] private Transform rangedPosition;
    [SerializeField] private float timeStayRanged;

    [Header("Other Configuration")]
    [SerializeField] private GetoAttackHandler attackHandler;


    private Coroutine stateTimer;

    private void OnEnable()
    {
        stateManager.OnAnimationComplete += TeleportBossToStatePosition;
    }

    private void OnDisable()
    {
        stateManager.OnAnimationComplete -= TeleportBossToStatePosition;
    }

    /// <summary>
    /// Changes Geto position depending on his current state.
    /// </summary>
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
            // Chage position
            transform.position = target.position;

            // Stop last state timer
            if (stateTimer != null)
                StopCoroutine(stateTimer);

            // Notify the attack script to start the attacking logic
            attackHandler.StartAttackSequence(stateManager.CurrentState == GetoStates.Melee ? 1 : 2);

            //Start current state timer
            stateTimer = StartCoroutine(StateTimerRoutine());
        }
    }

    /// <summary>
    /// Routine that handles the switching between states.
    /// </summary>
    /// <returns>Waits the state staying time</returns>
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
