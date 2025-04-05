using System.Collections;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // O objeto a ser spawnado
    //public Transform player; // Referência ao jogador
    [SerializeField]
    private float spawnInterval = 2f; // Tempo entre os spawns

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        if (prefab != null /*&& player != null*/)
        {
            GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
            //FollowPlayer follower = spawnedObject.GetComponent<FollowPlayer>();
            //if (follower != null)
            //{
            //    follower.SetTarget(player);
            //}
        }
    }
}
