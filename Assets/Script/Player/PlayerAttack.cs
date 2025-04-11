using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
        // Se o estado do player for "morto", não fazer nada.
        if (PlayerStateManager.Instance.State == PlayerState.Dead) return;

        grounded = anim.GetBool("IsGrounded");
        absVelocityX = anim.GetFloat("AbsVelocityX");

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Attack("LightAttack");
        //else if (Input.GetKeyDown(KeyCode.Mouse1))
            //Attack("HeavyAttack");

    }

    private void FixedUpdate()
    {
        if (isAttacking)
            rig.linearVelocity = Vector2.zero;
    }

    private void Attack(string attackType)
    {
        if (grounded && (absVelocityX <= 200 && absVelocityX >= -200) && !isAttacking)
            anim.SetTrigger(attackType);
    }

    #region Métodos chamados pelos Animation Events
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

