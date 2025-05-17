using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAvoidanceMovement : MonoBehaviour
{
    [Header("Avoidance Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Rigidbody2D parentRb;
    
    private Transform targetPlayer;
    private bool isAvoiding = false;

    
    void FixedUpdate()
    {
        if (isAvoiding && targetPlayer != null)
        {
            Vector2 direction = (transform.position - targetPlayer.position).normalized;
            parentRb.linearVelocity = new Vector2(direction.x * moveSpeed, parentRb.linearVelocityY);
        }
        else
        {
            parentRb.linearVelocity = new Vector2(0f, parentRb.linearVelocityY);
        }

        Debug.Log(parentRb.linearVelocityX);
    }

    public void StartAvoiding(Transform player)
    {
        targetPlayer = player;
        isAvoiding = true;
    }

    public void StopAvoiding()
    {
        isAvoiding = false;
        targetPlayer = null;
    }
}
