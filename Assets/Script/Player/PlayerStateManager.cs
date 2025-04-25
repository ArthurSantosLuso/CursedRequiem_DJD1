using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // This class manages the player's states.

    public static PlayerStateManager Instance { get; private set; }
    public PlayerState State { get; private set; } = PlayerState.Alive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Changes the player's state.
    /// </summary>
    /// <param name="newState">New state of the player</param>
    public void SetState(PlayerState newState)
    {
        State = newState;
    }
}
