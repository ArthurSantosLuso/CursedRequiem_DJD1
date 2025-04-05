using System.Collections;
using UnityEngine;
using OkapiKit;

public class DieEffect : AbilityBaseScript
{
    [Header("Configura��o do Efeito")]
    [SerializeField]
    private int dmgValue;


    private void Update()
    {
        // se o player estiver morto, n�o fazer nada.
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
