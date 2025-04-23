using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Esse script deve controlar a �rea de detec��o do inimigo. Se o inimigo que tiver este script detectar o jogador,
    // ele automaticamente mudar� de dire��o e ir� atr�s do jogador at� um ponto onde poder� atac�-lo, e ent�o atacar.


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
