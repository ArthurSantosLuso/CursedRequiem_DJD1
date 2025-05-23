using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GetoPositionHandle : MonoBehaviour
{
    [SerializeField] private GetoStateManager stateManager;

    [SerializeField] private Transform meleePosition;
    [SerializeField] private Transform rangedPosition;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        stateManager.OnAnimationComplete += TeleportBossToStatePosition;
    }

    private void OnDisable()
    {
        stateManager.OnAnimationComplete -= TeleportBossToStatePosition;
    }

    private void TeleportBossToStatePosition()
    {
        Transform target = null;

        switch (stateManager.CurrentState)
        {
            case GetoStates.Melee:
                target = meleePosition;
                break;
            case GetoStates.Ranged:
                target = rangedPosition;
                break;
        }

        if (target != null)
        {
            transform.position = target.position;
            Debug.Log("Boss teleportado para: " + target.name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (stateManager.CurrentState == GetoStates.Ranged)
                stateManager.SetState(GetoStates.Melee);
            else
                stateManager.SetState(GetoStates.Ranged);
        }
    }
}
