using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaScript : ValuesScript
{
    [SerializeField]
    private LifeManaBar barsScript;


    protected override void Start()
    {
        base.Start();
    }

    public void ReduceMana(int value)
    {
        currentValue -= value;
        calculatedValue = currentValue / base.defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }

    /// <summary>
    /// Verify if the player can restore mana.
    /// </summary>
    /// <param name="actualToRestore">The portion of the restoration value that was effectively applied to the player's mana.</param>
    /// <returns>Returns the possible amount to restore mana. </returns>
    public void CanRestore(float valueToRestore, out float actualToRestore)
    {
        // Se já está no máximo, não há nada para restaurar
        if (base.currentValue >= base.defaultValue)
        {
            actualToRestore = 0;
            return;
        }

        // Calcula o máximo que pode ser restaurado sem ultrapassar o limite
        float maxRestorable = base.defaultValue - base.currentValue;

        // O valor restaurado é o menor entre o solicitado e o máximo possível
        actualToRestore = Mathf.Min(valueToRestore, maxRestorable);
    }

    public void CallAdjustManaBar()
    {
        calculatedValue = currentValue / base.defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }


}
