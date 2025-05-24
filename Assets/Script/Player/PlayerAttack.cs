using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // This script handles the player's attack logic.
    // It still needs improvements. Example: Change the colliders to be the same as the ones the enemy uses for its attacks.

    private bool grounded;
    private float absVelocityX;
    private Animator anim;
    private bool isAttacking;

    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private GameObject slashFX;

    [Header("Attack Colliders")]
    [SerializeField]
    private GameObject lightAttackCollider;
    [SerializeField]
    private GameObject heavyAttackCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();

        lightAttackCollider.GetComponent<BoxCollider2D>().enabled = false;
        heavyAttackCollider.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        // If the player's state is "dead", do nothing.
        if (PlayerStateManager.Instance.State == PlayerState.Dead) return;

        grounded = anim.GetBool("IsGrounded");
        absVelocityX = anim.GetFloat("AbsVelocityX");

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Attack();

    }

    private void Attack()
    {
        if (grounded && Mathf.Abs(absVelocityX) < 0.1f && !isAttacking)
        {
            anim.SetTrigger("LightAttack_Idle");
        }
        else if(grounded && !isAttacking)
        {
            anim.SetTrigger("LightAttack_Move");
        }
    }

    #region Methods called by Animation Events
    private void EnableLightAttackCollider()
    {
        lightAttackCollider.GetComponent<BoxCollider2D>().enabled = true;
        slashFX.SetActive(false);
        slashFX.SetActive(true);

    }

    private void DisableLightAttackCollider()
    {
        lightAttackCollider.GetComponent<BoxCollider2D>().enabled = false;
        slashFX.SetActive(false);

    }

    private void EnableHeavyAttackCollider()
    {
        heavyAttackCollider.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void DisableHeavyAttackCollider()
    {
        heavyAttackCollider.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void ToggleOnIsAttacking()
    {
        isAttacking = true;
    }

    private void ToggleOffIsAttacking()
    {
        isAttacking = false;
    }

    #endregion
}
