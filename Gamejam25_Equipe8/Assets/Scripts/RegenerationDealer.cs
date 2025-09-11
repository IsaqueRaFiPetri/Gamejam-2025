using UnityEngine;

public class RegenerationDealer : MonoBehaviour
{
    [SerializeField] CharacterStatus statusPlayer;
    [SerializeField] bool destroyOnHit = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStatus player = other.GetComponent<PlayerStatus>();
        if (player != null)
        {
            player.RegenerateStats();

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
