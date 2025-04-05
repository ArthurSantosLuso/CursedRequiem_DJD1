using System;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField]
    private int dmgValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if (enemy)
        {
            enemy.TakeDamage(dmgValue);
        }
    }
}
