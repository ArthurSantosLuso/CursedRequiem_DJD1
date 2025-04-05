using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance { get; private set; }

    // O player começa vivo
    public PlayerState State { get; private set; } = PlayerState.Alive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetState(PlayerState newState)
    {
        State = newState;
    }
}
