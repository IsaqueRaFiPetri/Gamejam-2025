using UnityEngine;
using UnityEngine.Audio;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 10f;   // configurável no inspector
    [SerializeField] bool destroyOnHit = true;
    public AudioClip lazer_balldestrution;
    AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStatus player = other.GetComponent<PlayerStatus>();
        if (player != null)
        {          
            player.TakeDamageplayer(damage);
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 1f;
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
