using UnityEngine;

public class ValuesScript : MonoBehaviour
{
    [SerializeField]
    protected float defaultValue;
    public float currentValue;

    protected float calculatedValue;

    protected virtual void Start()
    {
        currentValue = defaultValue;
    }
}
