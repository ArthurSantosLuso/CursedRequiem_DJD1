using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;

    public void Initialize(Vector2 dir) => direction = dir;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Start() => Destroy(gameObject, lifeTime);

    private void FixedUpdate() => rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocityY);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }


}
