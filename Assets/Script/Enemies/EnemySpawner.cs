using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform spawnPoint;

    private float timer = 0f;
    private bool isSpawning = false;


    private void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        timer = 0f;
    }
}
