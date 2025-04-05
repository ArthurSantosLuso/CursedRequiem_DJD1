using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    private static int enemiesKilled = 0;

    public static void AddKill()
    {
        enemiesKilled++;
    }

    public static int GetKillCount()
    {
        return enemiesKilled;
    }
}
