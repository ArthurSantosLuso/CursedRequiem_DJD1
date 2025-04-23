using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaScript : ValuesScript
{
    // Esse scropt toma conta da logica da mana do jogador. Aumentar, diminuir e verificar se pode ser restaurada
    // são exemplos de coisas que esse script deve ser encarregado.

    [SerializeField]
    private LifeManaBar barsScript;


    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Reduz o valor da mana do jogaor
    /// </summary>
    /// <param name="value">Valor da redução</param>
    public void ReduceMana(int value)
    {
        currentValue -= value;
        calculatedValue = currentValue / base.defaultValue;
        barsScript.AdjustManaBar(calculatedValue);
    }

    /// <summary>
    /// Verifica se o jogador pode restaurar mana.
    /// </summary>
    /// <param name="valueToRestore">Valor default de restauração da mana.</param>
    /// <param name="actualToRestore">A parte do valor de restauração que vai efetivamente ser aplicada a mana do jogador.</param>
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
