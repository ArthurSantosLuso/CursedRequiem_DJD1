using System.Collections;
using UnityEngine;

public class GetoAttackHandler : MonoBehaviour
{
    // This script takes care of Geto's melee & ranged attack. Animation, delay and activating the attack FX.

    [SerializeField] private GetoStateManager stateManager;

    [Header("States Delays")]
    [SerializeField] private float meleeDelay = 2f;
    [SerializeField] private float rangedDelay = 3f;

    [Header("Attack Animators")]
    [SerializeField] private Animator meleeAnimator;
    [SerializeField] private Animator rangedAnimator;

    [Header("Attack Visuals")]
    [SerializeField] private GameObject meleeAttackFX;
    [SerializeField] private GameObject rangedAttackFX;

    /// <summary>
    /// Starts the attack event depending on the current state of Geto.
    /// </summary>
    public void StartAttackSequence()
    {
        switch (stateManager.CurrentState)
        {
            case GetoStates.Melee:
                StartCoroutine(AttackAfterDelay(meleeDelay));
                break;

            //case GetoStates.Ranged:
            //    StartCoroutine(AttackAfterDelay(rangedDelay, rangedAnimator, "RangedAttack"));
            //    break;
        }
    }

    /// <summary>
    /// Handles the attack animation and enable/disable the Fx.
    /// </summary>
    /// <param name="delay">They delay of the attack</param>
    /// <returns></returns>
    private IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        switch (stateManager.CurrentState)
        {
            case GetoStates.Melee:
                meleeAttackFX.SetActive(true);

                // Wait melee attack full animation time
                yield return new WaitForSeconds(0.33f);
                // Disable melee attack fx
                DisableAttackFX();

                break;
            //case GetoStates.Ranged:
            //    rangedAnimator.Play("Ranged", -1, 0f);
            //    break;
        }
    }

    /// <summary>
    /// Disables ranged & melee attack visual.
    /// </summary>
    private void DisableAttackFX()
    {
        meleeAttackFX.SetActive(false);
    }
}
