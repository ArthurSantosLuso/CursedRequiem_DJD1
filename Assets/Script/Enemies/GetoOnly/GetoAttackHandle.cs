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
    public void StartAttackSequence(int times = 1)
    {
        switch (stateManager.CurrentState)
        {
            case GetoStates.Melee:
                StartCoroutine(AttackAfterDelay(meleeDelay, times));
                break;
            case GetoStates.Ranged:
                StartCoroutine(AttackAfterDelay(rangedDelay, times));
                break;
        }
    }

    /// <summary>
    /// Handles the attack animation and enable/disable the Fx.
    /// </summary>
    /// <param name="delay">They delay before the attack happen</param>
    /// <returns></returns>
    private IEnumerator AttackAfterDelay(float delay, int repeatTimes)
    {
        for (int i = 0; i < repeatTimes; i++)
        {
            if (stateManager.CurrentState == GetoStates.Ranged)
            {
                rangedAttackFX.GetComponent<SpriteRenderer>().enabled = true;
            }

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
                case GetoStates.Ranged:
                    // Activate the ranged attack animation
                    Animator rangedAnimator = rangedAttackFX.GetComponent<Animator>();
                    rangedAnimator.enabled = true;
                    rangedAnimator.Play("Portal", 0, 0f);
                    // Wait ranged attack full animation time
                    yield return new WaitForSeconds(1.3f);
                    // Disable ranged attack fx
                    DisableAttackFX();
                    break;
            }
        }
    }

    /// <summary>
    /// Disables ranged & melee attack visual.
    /// </summary>
    private void DisableAttackFX()
    {
        meleeAttackFX.SetActive(false);
        rangedAttackFX.GetComponent<Animator>().enabled = false;
        rangedAttackFX.GetComponent<SpriteRenderer>().enabled = false;
    }
}
