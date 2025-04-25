using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaScript : ValuesScript
{
    // This script handles the player's mana logic. Increasing, decreasing, and checking if it can be restored
    // are examples of things this script should take care of.

    [SerializeField]
    private LifeManaBar barsScript;


    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Reduces the player's mana value.
    /// </summary>
    /// <param name="value">Reduction value</param>
    public void ReduceMana(int value)
    {
        currentValue -= value;
        calculatedValue = currentValue / base.defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }

    /// <summary>
    /// Checks if the player can restore mana.
    /// </summary>
    /// <param name="valueToRestore">Default mana restoration value.</param>
    /// <param name="actualToRestore">The actual value of restoration that will be applied to the player's mana.</param>
    public void CanRestore(float valueToRestore, out float actualToRestore)
    {
        // If it's already at maximum, there's nothing to restore
        if (base.currentValue >= base.defaultValue)
        {
            actualToRestore = 0;
            return;
        }

        // Calculates the maximum that can be restored without exceeding the limit
        float maxRestorable = base.defaultValue - base.currentValue;

        // The restored value is the smaller of the requested value and the maximum possible
        actualToRestore = Mathf.Min(valueToRestore, maxRestorable);
    }

    public void CallAdjustManaBar()
    {
        calculatedValue = currentValue / base.defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }
}
