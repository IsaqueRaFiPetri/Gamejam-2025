using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 10f;   // configurável no inspector
    [SerializeField] bool destroyOnHit = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStatus player = other.GetComponent<PlayerStatus>();
        if (player != null)
        {
            player.TakeDamageplayer(damage);

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
