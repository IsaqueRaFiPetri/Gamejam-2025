using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStats;

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (Troca_Personagens.instance.isHappy)
            {
                enemy.TakeDamageenemy(playerStats.damage -1);
            }
            else if (Troca_Personagens.instance.isBrave)
            {
                enemy.TakeDamageenemy(playerStats.damage * 2);
            }
            else 
            {
                enemy.TakeDamageenemy(playerStats.damage);
            }
        }
    }
}
