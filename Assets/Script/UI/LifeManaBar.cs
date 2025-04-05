using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LifeManaBar : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Image manaBar;      


    public void AdjustLifeBar(float calculatedLife)
    {
        lifeBar.fillAmount = Mathf.MoveTowards(lifeBar.fillAmount, calculatedLife, 1f);
    }

    public void AdjustManaBar(float calculatedMana)
    {
        manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, calculatedMana, 1f);
    }

}
