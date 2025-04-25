using UnityEngine;

public class ValuesScript : MonoBehaviour
{
    // This script serves as a parent for all scripts that in some way need "base value" and "current value",
    // such as health and mana scripts.

    [SerializeField]
    protected float defaultValue;
    public float currentValue;

    protected float calculatedValue;

    protected virtual void Start()
    {
        currentValue = defaultValue;
    }
}
