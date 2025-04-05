using UnityEngine;
using System.Collections;

public class SleepEffect : AbilityBaseScript
{
    [Header("Configuração do Sono")]
    public float sleepDuration = 2f; // Tempo que os inimigos ficam parados

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
            EnemyStates previousState = enemyStateManager.CurrentState; // Salva o estado antes de dormir
            enemyStateManager.SetState(EnemyStates.Sleeping); // Muda para "Sleeping"
            sr.color = Color.blue;

            yield return new WaitForSeconds(sleepDuration); // Espera o tempo do sono

            // Se o inimigo ainda estiver vivo, retorna ao estado anterior
            if (enemy != null)
            {
                enemyStateManager.SetState(previousState);
                sr.color = Color.white;
            }
        }
    }
}

