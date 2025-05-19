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

        // Verifica se h� um collider de limites
        if (boundary != null)
        {
            Bounds bounds = boundary.bounds;
            minBounds = bounds.min;
            maxBounds = bounds.max;
        }

        // Calcula metade da largura e altura da c�mera
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Define a posi��o alvo com offset
        Vector3 targetPosition = target.position + (Vector3)offset;
        targetPosition.z = transform.position.z; // Mant�m a profundidade da c�mera

        // Suaviza o movimento da c�mera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Restringe a posi��o dentro dos limites, considerando o tamanho da c�mera
        if (boundary != null)
        {
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            smoothedPosition = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }

        // Aplica a posi��o final � c�mera
        transform.position = smoothedPosition;
    }
}
