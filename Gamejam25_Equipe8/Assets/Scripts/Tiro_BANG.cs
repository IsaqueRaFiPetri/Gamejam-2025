using UnityEngine;

public class Tiro_BANG : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public GameObject projectilePrefab;
    public Transform firePosition;
    public float projectileSpeed = 20f;
    public float fireRate = 0.5f;

    float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;

            Vector2 direction = (mouseWorldPosition - firePosition.position).normalized;

            Shoot(direction);

            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePosition.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;

           
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        Destroy(projectile, 1.5f);
    }
}
