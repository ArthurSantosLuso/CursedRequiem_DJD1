using UnityEngine;
using UnityEngine.UI;
public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;

    public void AdjustLifeBar(float calculatedLife)
    {
        lifeBar.fillAmount = Mathf.MoveTowards(lifeBar.fillAmount, calculatedLife, 1f);
    }
}
