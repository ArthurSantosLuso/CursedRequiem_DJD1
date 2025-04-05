using UnityEngine;
using OkapiKit;

public class SleepPower : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    void Update()
    {
        var a = enemy.GetComponent<OkapiKit.MovementPath>();

    }
}
