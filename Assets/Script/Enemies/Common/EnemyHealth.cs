using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : ValuesScript
{
    // This script should manage the enemy's health, avoiding redundancy with the parent script "ValuesScript".

    [SerializeField]
    private EnemyLifeBar barsScript;

    [SerializeField]
    private Transform player;

    private bool isFacingRight = true;

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
        ForceFlip();


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

    // Change this later
    void ForceFlip()
    {
        if (player == null) return;

        float directionToPlayer = player.position.x - transform.position.x;

        if (directionToPlayer > 0 && !isFacingRight)
        {
            RotateTowards(true);
        }
        else if (directionToPlayer < 0 && isFacingRight)
        {
            RotateTowards(false);
        }
    }

    void RotateTowards(bool faceRight)
    {
        isFacingRight = faceRight;

        float yRotation = faceRight ? 0f : 180f;

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
