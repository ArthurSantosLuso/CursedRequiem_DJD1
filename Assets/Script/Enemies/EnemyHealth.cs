using UnityEngine;
using OkapiKit;

public class EnemyHealth : ValuesScript
{
    [SerializeField]
    private EnemyLifeBar barsScript;

    protected override void Start()
    {
        base.Start();
    }

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
