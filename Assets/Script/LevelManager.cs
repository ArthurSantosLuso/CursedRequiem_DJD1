using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Update()
    {
        if (EnemyCount.GetKillCount() == 12)
        {
            SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        }
    }
}
