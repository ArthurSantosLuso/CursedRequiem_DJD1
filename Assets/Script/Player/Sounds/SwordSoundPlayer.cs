using UnityEngine;

public class SwordSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip swordSound;

    public void PlaySwordSound()
    {
        if (audioSource != null && swordSound != null)
        {
            audioSource.PlayOneShot(swordSound);
        }
    }
}
