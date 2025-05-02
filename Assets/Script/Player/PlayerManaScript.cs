using UnityEngine;

public class PlayerManaScript : ValuesScript
{
    // This script handles the player's mana logic: reducing, restoring, and draining over time.

    [Header("References")]
    [SerializeField] private LifeManaBar barsScript;

    [Header("Mana Drain Settings")]
    [SerializeField] private float manaDrainPerSecond = 1f;
    [SerializeField] private bool isDrainingMana = false;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        HandleManaDrainOverTime();
    }

    /// <summary>
    /// Continuously drains mana per second if enabled.
    /// </summary>
    private void HandleManaDrainOverTime()
    {
        if (!isDrainingMana || currentValue <= 0) return;

        float manaToDrain = manaDrainPerSecond * Time.deltaTime;
        ReduceMana(manaToDrain);
    }

    /// <summary>
    /// Reduces the player's mana value and updates the UI.
    /// </summary>
    /// <param name="value">Amount to reduce.</param>
    public void ReduceMana(float value)
    {
        currentValue -= value;
        currentValue = Mathf.Clamp(currentValue, 0, defaultValue); // Prevent negative mana
        calculatedValue = currentValue / defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }

    /// <summary>
    /// Checks if the player can restore mana.
    /// </summary>
    /// <param name="valueToRestore">Requested value to restore.</param>
    /// <param name="actualToRestore">The final restored value.</param>
    public void CanRestore(float valueToRestore, out float actualToRestore)
    {
        if (currentValue >= defaultValue)
        {
            actualToRestore = 0f;
            return;
        }

        float maxRestorable = defaultValue - currentValue;
        actualToRestore = Mathf.Min(valueToRestore, maxRestorable);
    }

    /// <summary>
    /// Manually updates the mana bar UI.
    /// </summary>
    public void CallAdjustManaBar()
    {
        calculatedValue = currentValue / defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }

    /// <summary>
    /// Enables or disables automatic mana drain.
    /// </summary>
    /// <param name="shouldDrain">Whether mana should be drained over time.</param>
    public void SetManaDrainState(bool shouldDrain)
    {
        isDrainingMana = shouldDrain;
    }
}
