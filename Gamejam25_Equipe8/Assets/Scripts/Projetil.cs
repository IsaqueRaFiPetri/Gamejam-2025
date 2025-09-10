using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStats;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
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
        }
    }
}
