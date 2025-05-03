using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;

public class RangedEnemyShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate;

    private float timer = 0f;
    private Transform target;
    private bool isShooting = false;


    private void Update()
    {
        if (!isShooting || target == null) return;

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    public void StartShooting(Transform player)
    {
        target = player;
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
        target = null;
        timer = 0f;
    }

    private void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = (target.position - shootPoint.position).normalized;

        Projectile projectile = proj.GetComponent<Projectile>();
        if(projectile != null)
        {
            projectile.Initialize(direction);
        }
    }
}
