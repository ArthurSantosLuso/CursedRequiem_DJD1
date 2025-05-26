using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    public static EnemyCount Instance;

    [SerializeField] private int enemiesToKill = 5;
    [SerializeField] private string nextSceneName = "CenaVitoria";

    private int deadEnemies = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EnemyDied()
    {
        deadEnemies++;

        if (deadEnemies >= enemiesToKill)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
