using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStats;
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
        audioSource.volume = 0.65f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (acertouAlvo) return;

            print(other.gameObject);
            Enemy enemy = other.GetComponent<Enemy>();

            
            if (enemy != null)
            {
                if (Troca_Personagens.instance.isHappy)
                {
                    enemy.TakeDamageenemy(playerStats.damage - 1);
                }
                else if (Troca_Personagens.instance.isBrave)
                {
                    enemy.TakeDamageenemy(playerStats.damage * 2);
                }
                else if (Troca_Personagens.instance.isFear)
                {
                    enemy.TakeDamageenemy(playerStats.damage / 2);
                }
                else
                {
                    enemy.TakeDamageenemy(playerStats.damage);
                }

                acertouAlvo = true;

                if (acertouAlvo == true)
                    Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Boss"))
        {
            BossController boss = other.GetComponentInParent<BossController>();

            if (Troca_Personagens.instance.isHappy)
            {
                boss.TakeDamage(playerStats.damage - 1);
            }
            else if (Troca_Personagens.instance.isBrave)
            {
                boss.TakeDamage(playerStats.damage * 2);
            }
            else if (Troca_Personagens.instance.isFear)
            {
                boss.TakeDamage(playerStats.damage / 2);
            }
            else
            {
                boss.TakeDamage(playerStats.damage);
            }
            acertouAlvo = true;
        }
        if (acertouAlvo)
        {
            if (Bang != null)
            {
                audioSource.PlayOneShot(Bang);
            }            
            Destroy(gameObject, Bang != null ? Bang.length : 0f);
        }
    }
}
