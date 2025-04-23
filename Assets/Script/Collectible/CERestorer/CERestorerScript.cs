
using UnityEngine;

public class CERestorerScript : MonoBehaviour
{
    // Esse script deve tomar conta do coletavel que restaura mana do jogaor


    // Esse script precisa ser alterado, do jeito que está é esse script que restaura a mana do jogador,
    // mas a parte de restauração devia ser feita pelo proprio script da mana, com um metodo de aumentar o
    // valor da mana por exemplo.

    [SerializeField]
    private float valeuToRestore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManaScript playerMana = collision.gameObject.GetComponent<PlayerManaScript>();

        if (playerMana)
        {
            float restoredMana;

            playerMana.CanRestore(valeuToRestore, out restoredMana);
            playerMana.currentValue += restoredMana;
            playerMana.CallAdjustManaBar();

            Destroy(gameObject);
        }
    }
}
