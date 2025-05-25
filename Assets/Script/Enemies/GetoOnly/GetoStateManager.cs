using System.Collections;
using UnityEngine;

public class GetoStateManager : MonoBehaviour
{
    // This script takes care of Geto's states. Waiting, Melee and Ranged.

    public GetoStates CurrentState { get; private set; }

    public delegate void OnStateAnimationComplete();
    public event OnStateAnimationComplete OnAnimationComplete;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetState(GetoStates.Ranged);
    }

    /// <summary>
    /// Set new a state for Geto.
    /// </summary>
    /// <param name="newState">The new required state</param>
    public void SetState(GetoStates newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GetoStates.Ranged:
                animator.SetTrigger("Tp");
                break;
            case GetoStates.Melee:
                animator.SetTrigger("Tp");
                break;
        }
    }

    // This metodh will be called after the teleport animation ends by animation event
    public void NotifyAnimationFinished()
    {
        OnAnimationComplete?.Invoke();
    }
}
