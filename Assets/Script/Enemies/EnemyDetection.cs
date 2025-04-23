using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Esse script deve controlar a área de detecção do inimigo. Se o inimigo que tiver este script detectar o jogador,
    // ele automaticamente mudará de direção e irá atrás do jogador até um ponto onde poderá atacá-lo, e então atacar.


    [SerializeField]
    private Transform detectionPoint;
    [SerializeField]
    private float detectionRange = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (detectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
        }
    }
}
