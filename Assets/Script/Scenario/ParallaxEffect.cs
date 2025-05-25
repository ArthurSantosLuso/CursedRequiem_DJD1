using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float lenght;
    private float startPos;

    private Transform cam;
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        float rePos = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (rePos > startPos + lenght)
        {
            startPos += lenght;
        }
        else if(rePos < startPos - lenght)
        {
            startPos -= lenght;
        }
    }
}
