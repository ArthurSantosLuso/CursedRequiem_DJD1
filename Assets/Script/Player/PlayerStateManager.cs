using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // Essa class toma conta dos estados do jogador.

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
    /// Muda o estado do jogador.
    /// </summary>
    /// <param name="newState">Novo estado do jogador</param>
    public void SetState(PlayerState newState)
    {
        State = newState;
    }
}
