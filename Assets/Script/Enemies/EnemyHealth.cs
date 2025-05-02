using UnityEngine;

public class EnemyHealth : ValuesScript
{
    // This script should manage the enemy's health, avoiding redundancy with the parent script "ValuesScript".

    [SerializeField]
    private EnemyLifeBar barsScript;

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Function to deal damage to the enemy.
    /// </summary>
    /// <param name="dmgValue">Damage value</param>
    public void TakeDamage(int dmgValue)
    {
        base.currentValue -= dmgValue;

        if (barsScript != null)
        {
            base.calculatedValue = base.currentValue / base.defaultValue;
            barsScript.AdjustLifeBar(base.calculatedValue);
        }

        if (currentValue <= 0)
        {
            Destroy(gameObject);
        }

    }
}
