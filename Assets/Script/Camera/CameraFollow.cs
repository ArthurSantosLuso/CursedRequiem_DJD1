using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private Collider2D boundary;

    private Camera cam;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (boundary != null)
        {
            Bounds bounds = boundary.bounds;
            minBounds = bounds.min;
            maxBounds = bounds.max;
        }

        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + (Vector3)offset;
        targetPosition.z = transform.position.z;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        if (boundary != null)
        {
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            smoothedPosition = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }

        transform.position = smoothedPosition;
    }
}
