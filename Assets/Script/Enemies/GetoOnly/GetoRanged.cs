using UnityEngine;

public class GetoRanged : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 nextPosition = transform.position;

        // Take players position
        nextPosition.x = player.position.x;

        // Make the portal follow the player
        transform.position = nextPosition;
    }
}
