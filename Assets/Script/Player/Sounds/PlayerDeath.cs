using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;
    public Animator animator;
    public MonoBehaviour[] scriptsToDisable; 

    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

       
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        
        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }

        
        Invoke("RestartScene", 2f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
