using UnityEngine;

public class Tiro_BANG : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public GameObject projectilePrefab;
    public Transform firePosition;
    public float projectileSpeed = 20f;
    public float fireRate = 0.5f;
    float nextFireTime = 0f;

    Troca_Personagens troca;

    private void Start()
    {
        troca = FindFirstObjectByType<Troca_Personagens>();
        mainCamera = FindFirstObjectByType<Camera>();
    }

    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 lookDir = mouseWorldPosition - firePosition.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        firePosition.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Vector2 direction = lookDir.normalized;

            if (Troca_Personagens.instance.isFear)
            {
                direction *= -1;
            }

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
