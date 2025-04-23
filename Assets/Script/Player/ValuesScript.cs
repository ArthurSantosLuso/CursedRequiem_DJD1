using UnityEngine;

public class ValuesScript : MonoBehaviour
{
    // Esse script serve como pai para todos os scripts que de alguma forma precisam de "valor base" e "valor atual"
    // como scripts de vida e mana.

    [SerializeField]
    protected float defaultValue;
    public float currentValue;

    protected float calculatedValue;

    protected virtual void Start()
    {
        currentValue = defaultValue;
    }
}
