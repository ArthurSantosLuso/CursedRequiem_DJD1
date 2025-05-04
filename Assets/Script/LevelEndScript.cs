using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScript : MonoBehaviour
{
    private Collider2D exitCollider;

    private void Start()
    {
        exitCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthScript player = collision.GetComponent<PlayerHealthScript>();
         
        if (player)
        {
            SceneManager.LoadScene(0);
        }
            
        
    }
}
