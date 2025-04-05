using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float waitTime = 2f;

    private Transform targetPoint;
    private bool isWaiting = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPoint = pointA;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isWaiting)
            {
                Vector2 direction = (targetPoint.position - transform.position).normalized;
                rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
                Flip(rb.linearVelocity.x);

                if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
                {
                    rb.linearVelocity = Vector2.zero;
                    isWaiting = true;
                    yield return new WaitForSeconds(waitTime);
                    targetPoint = (targetPoint == pointA) ? pointB : pointA;
                    isWaiting = false;
                }
            }
            yield return null;
        }
    }

    void Flip(float moveDir)
    {
        if (moveDir > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (moveDir < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
}
