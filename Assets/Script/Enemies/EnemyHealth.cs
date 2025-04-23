using UnityEngine;
using OkapiKit;

public class EnemyHealth : ValuesScript
{
    // Esse script deve controlar a vida do inimigo, evitando redund�ncia com o script pai "ValuesScript".

    [SerializeField]
    private EnemyLifeBar barsScript;

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Fun��o para causar dano no inimgo.
    /// </summary>
    /// <param name="dmgValue">Valor do dano</param>
    public void TakeDamage(int dmgValue)
    {
        base.currentValue -= dmgValue;
        base.calculatedValue = base.currentValue / base.defaultValue;
        barsScript.AdjustLifeBar(base.calculatedValue);

        if (currentValue <= 0)
        {
            EnemyCount.AddKill();
            Destroy(gameObject);
        }

    }
}
