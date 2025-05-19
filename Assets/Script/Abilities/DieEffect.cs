using System.Collections;
using UnityEngine;

public class DieEffect : AbilityBaseScript
{
    [Header("Effect Configuration")]
    [SerializeField]
    private int dmgValue;

    private void Update()
    {
        // If the player is dead, do nothing.
        if (PlayerStateManager.Instance.State == PlayerState.Dead) return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (base.manaScript.currentValue >= base.manaCost)
            {
                base.manaScript.ReduceMana(base.manaCost);
                StartCoroutine(ApplyEffect(effectDuration));
            }
            else
            {
                Debug.Log("Not enough mana!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if (enemy)
        {
            enemy.TakeDamage(dmgValue);
        }
    }
}
