using UnityEngine;
using System.Collections;

public class AbilityBaseScript : MonoBehaviour
{
    // Base class for player abilities. 
    // Everything common between abilities should be placed here.

    [Header("Base configurations")]
    [SerializeField]
    protected SpriteRenderer effectSprite; // The sprite that will be activated for the effect
    [SerializeField]
    protected Collider2D effectCollider; // The Collider2D for the effect area
    [SerializeField]
    protected float effectDuration = 0.5f; // The duration the effect remains active
    [SerializeField]
    protected int manaCost;
    [SerializeField]
    protected PlayerManaScript manaScript;


    protected void Start()
    {
        if (effectSprite != null)
        {
            effectSprite.enabled = false; // Ensures the effect starts invisible
        }

        if (effectCollider != null)
        {
            effectCollider.enabled = false; // Disables the collider initially
        }

    }

    protected IEnumerator ApplyEffect(float time)
    {
        // Activates the visual effect and the collider
        if (effectSprite != null)
        {
            effectSprite.enabled = true;
        }

        if (effectCollider != null)
        {
            effectCollider.enabled = true;
        }

        // Waits a little before hiding the effect
        yield return new WaitForSeconds(0.5f);

        if (effectSprite != null)
        {
            effectSprite.enabled = false;
        }

        if (effectCollider != null)
        {
            effectCollider.enabled = false; // Disables the collider after the effect
        }
    }
}
