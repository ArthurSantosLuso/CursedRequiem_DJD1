using UnityEngine;
using System.Collections;

public class AbilityBaseScript : MonoBehaviour
{
    [Header("Base configurations")]
    [SerializeField]
    protected SpriteRenderer effectSprite; // O sprite que será ativado no efeito
    [SerializeField]
    protected Collider2D effectCollider; // O Collider2D da área de efeito
    [SerializeField]
    protected float effectDuration = 0.5f; // Tempo que o efeito fica ativo
    [SerializeField]
    protected int manaCost;
    [SerializeField]
    protected PlayerManaScript manaScript;
    

    protected void Start()
    {
        if (effectSprite != null)
        {
            effectSprite.enabled = false; // Garante que o efeito começa invisível
        }

        if (effectCollider != null)
        {
            effectCollider.enabled = false; // Desativa o collider inicialmente
        }

    }

    protected IEnumerator ApplyEffect(float time)
    {
        // Ativa o efeito visual e o collider
        if (effectSprite != null)
        {
            effectSprite.enabled = true;
        }

        if (effectCollider != null)
        {
            effectCollider.enabled = true;
        }

        // Espera um pouco antes de esconder o efeito
        yield return new WaitForSeconds(0.5f);

        if (effectSprite != null)
        {
            effectSprite.enabled = false;
        }

        if (effectCollider != null)
        {
            effectCollider.enabled = false; // Desativa o collider após o efeito
        }
    }
}
