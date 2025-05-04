using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundsScript : MonoBehaviour
{
    private Collider2D boundsCollider;

    private void Start()
    {
        boundsCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthScript isPlayer = collision.GetComponent<PlayerHealthScript>();
        EnemyHealth isEnemy = collision.GetComponent<EnemyHealth>();

        // Player fell off the bounds of the level
        if (isPlayer)
        {
            SceneManager.LoadScene(0);
        }

        if (isEnemy)
        {
            isEnemy.Kill();
        }

    }
}
