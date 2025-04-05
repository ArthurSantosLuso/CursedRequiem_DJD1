using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform target;

    //public void SetTarget(Transform newTarget)
    //{
    //    target = newTarget;
    //}

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
