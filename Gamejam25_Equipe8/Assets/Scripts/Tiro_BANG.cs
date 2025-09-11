using UnityEngine;

public class Tiro_BANG : MonoBehaviour
{
    Camera mainCamera;
    public GameObject projectilePrefab;
    public Transform firePosition;
    public float projectileSpeed = 20f;
    public float fireRate = 0.5f;
    float nextFireTime = 0f;
    [SerializeField] float cost = -1f;
    [SerializeField] CharacterStatus playerStats;
    public AudioClip Tiro;
    AudioSource audioSource;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0.65f;
    }

    void Update()
    {        
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 lookDir = mouseWorldPosition - firePosition.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        firePosition.rotation = Quaternion.Euler(0, 0, angle);
        Vector2 direction = lookDir.normalized;

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {           
            if (Troca_Personagens.instance.isFear)
            {
                direction *= -1;
            }

            Shoot(direction);            
            if (Troca_Personagens.instance.isSad)
            {
                nextFireTime = Time.time + fireRate + 0.5f;
            }
            if (Troca_Personagens.instance.isFear)
            {
                nextFireTime = Time.time + fireRate - 0.5f;
            }
            if (Troca_Personagens.instance.isBrave)
            {
                nextFireTime = Time.time + fireRate + 0.5f;
            }
            else
            {
                nextFireTime = Time.time + fireRate;
            }
        }
       
    }


    void Shoot(Vector2 direction)
    {
        
        if (!Troca_Personagens.instance.isBrave)
        {
            cost = 4f;
        }
        else if(Troca_Personagens.instance.isBrave)
        {
            cost = 5f;
        }
        else if (Troca_Personagens.instance.isHappy)
        {
            cost = 3f;
        }
        else if (Troca_Personagens.instance.isFear)
        {
            cost = 2f;
        }
        else if (Troca_Personagens.instance.isSad)
        {
            cost = 5f;
        }

        if (playerStats.energy >= cost)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePosition.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            PlayerStatus.instance.ReduceEnergy(cost);
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;               
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            if (Tiro != null)
            {
                audioSource.PlayOneShot(Tiro);
            }                
            Destroy(projectile, 1.5f);
        }
    }    
}
