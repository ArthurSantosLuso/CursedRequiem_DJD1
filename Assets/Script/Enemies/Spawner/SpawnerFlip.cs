using UnityEngine;

public class SpawnerFlip : MonoBehaviour
{
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private EnemySpawnerController spawnController;

    private Transform player;

    private void Update()
    {
        player = spawnController.PlayerReference;
        if (player == null) return;

        HandleFlip();
    }

    private void HandleFlip()
    {
        float dirToPlayer = player.position.x - transform.position.x;
        bool isAvoiding = spawnController.IsAvoidingPlayer;

        float yRotation;

        if (isAvoiding)
        {
            // Flip away from player
            yRotation = dirToPlayer < 0 ? 0f : 180f;
        }
        else if (spawnController.IsPlayerInSpawnArea)
        {
            // Flip toward player
            yRotation = dirToPlayer < 0 ? 180f : 0f;
        }
        else
        {
            return; // Don't flip if player isn't in any area
        }

        objectToRotate.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}

