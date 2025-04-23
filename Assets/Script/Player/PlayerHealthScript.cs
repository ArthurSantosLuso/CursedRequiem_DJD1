using UnityEngine;
using OkapiKit;
public class PlayerHealthScript : ValuesScript
{
    // Esse script toma conta da vida do jogador.
    // Aplicar dano e matar o jogador são as coisas que devems ser tratadas aqui.

    [SerializeField]
    private LifeManaBar barsScript;
    [SerializeField]
    private PlayerMovement moveScript;

    private Animator anin;

    protected override void Start()
    {
        base.Start();
        anin = GetComponent<Animator>();
    }

    private void Update()
    {
        // não fazer nada se o player estiver morto.
        if (PlayerStateManager.Instance.State == PlayerState.Dead) return;

        if(currentValue <= 0)
        {
            Die();    
        }    
    }

    /// <summary>
    /// Aplica dano ao jogador.
    /// </summary>
    /// <param name="damage">Valor do dano a ser aplicado</param>
    public void DamagePlayer(int damage)
    {
        base.currentValue -= damage;
        base.calculatedValue = base.currentValue / base.defaultValue;
        barsScript.AdjustLifeBar(base.calculatedValue);
    }

    /// <summary>
    /// Mata o jogador.
    /// </summary>
    private void Die()
    {
        // Mudar o estado do player para morto.
        PlayerStateManager.Instance.SetState(PlayerState.Dead);
        anin.SetTrigger("Die");
        this.enabled = false;
        moveScript.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        Destroy(gameObject, 2f);
    }
}
