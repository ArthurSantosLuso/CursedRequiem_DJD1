using UnityEngine;

public class FlipDustEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem dust;
    [SerializeField]
    private Rigidbody2D rig;



    // Update is called once per frame
    void Update()
    {
        if(rig.linearVelocityX < 1)
        {
            dust.Play();
        }

        if(rig.linearVelocityX > 0)
        {
            dust.Play();
        }
    }
}
