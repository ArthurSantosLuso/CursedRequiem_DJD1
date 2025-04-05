using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField]
    private bool isInBox;

    void Update()
    {
        if (isInBox)
        {
            Debug.Log("Found in box!");
        }
        else
        {
            Debug.Log("Not in box!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInBox = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInBox = false;
        }
    }
}
