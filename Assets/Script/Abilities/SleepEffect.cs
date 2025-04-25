using UnityEngine;
using System.Collections;

public class SleepEffect : AbilityBaseScript
{
    [Header("Sleep Configuration")]
    public float sleepDuration = 2f; // The time enemies stay still

    private void Update()
    {
        if (PlayerStateManager.Instance.State == PlayerState.Dead) return;

        if (Input.GetKeyDown(KeyCode.R))
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
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(SleepEnemy(other));
        }
    }

    private IEnumerator SleepEnemy(Collider2D enemy)
    {
        EnemyStateManager enemyStateManager = enemy.GetComponent<EnemyStateManager>();
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();

        if (enemyStateManager != null)
        {
            EnemyStates previousState = enemyStateManager.CurrentState; // Saves the state before sleeping
            enemyStateManager.SetState(EnemyStates.Sleeping); // Changes to "Sleeping"
            sr.color = Color.blue;

            yield return new WaitForSeconds(sleepDuration); // Waits for the sleep duration

            // If the enemy is still alive, return to the previous state
            if (enemy != null)
            {
                enemyStateManager.SetState(previousState);
                sr.color = Color.white;
            }
        }
    }
}
