using UnityEngine;
public class PlayerHealthScript : ValuesScript
{
    // This script handles the player's health.
    // Applying damage and killing the player are the things that should be handled here.

    [SerializeField]
    private LifeManaBar barsScript;
    [SerializeField]
    private PlayerMovement moveScript;

    private Animator anin;

    protected override void Start()
    {
        base.Start();
        anin = GetComponent<Animator>();
    }

    private void Update()
    {
        // Do nothing if the player is dead.
        if (PlayerStateManager.Instance.State == PlayerState.Dead) return;

        if (currentValue <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Applies damage to the player.
    /// </summary>
    /// <param name="damage">The damage value to be applied</param>
    public void DamagePlayer(int damage)
    {
        base.currentValue -= damage;
        base.calculatedValue = base.currentValue / base.defaultValue;
        barsScript.AdjustLifeBar(base.calculatedValue);
    }

    /// <summary>
    /// Kills the player.
    /// </summary>
    private void Die()
    {
        // Change the player's state to dead.
        PlayerStateManager.Instance.SetState(PlayerState.Dead);
        anin.SetTrigger("Die");
        this.enabled = false;
        moveScript.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        Destroy(gameObject, 2f);
    }
}
