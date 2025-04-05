
using UnityEngine;

public class CERestorerScript : MonoBehaviour
{

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
