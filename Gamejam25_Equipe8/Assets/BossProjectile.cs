using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] CharacterStatus status;
    public AudioClip Bang;
    AudioSource audioSource;
    bool acertouAlvo = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0.4f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (acertouAlvo) return;

            PlayerStatus player = other.GetComponent<PlayerStatus>();

            if (player != null)
            {
                if (Troca_Personagens.instance.isHappy)
                {
                    player.TakeDamageplayer(status.damage);
                }
            }
        }
    }
}