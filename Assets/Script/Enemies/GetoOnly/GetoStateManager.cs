using UnityEngine;

public class GetoStateManager : MonoBehaviour
{
    public GetoStates CurrentState { get; private set; }

    public delegate void OnStateAnimationComplete();
    public event OnStateAnimationComplete OnAnimationComplete;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetState(GetoStates.Ranged);
    }

    public void SetState(GetoStates newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            //case GetoStates.Waiting:
            //    animator.Play("Idle");
            //    break;
            case GetoStates.Ranged:
                animator.SetTrigger("Tp");
                break;
            case GetoStates.Melee:
                animator.SetTrigger("Tp");
                break;
        }
    }

    // Este metodo vai ser chamado no final da animação pelo animation event
    public void NotifyAnimationFinished()
    {
        OnAnimationComplete?.Invoke();
    }
}
