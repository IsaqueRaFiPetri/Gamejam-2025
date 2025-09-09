using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStats;    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BANG();           
        }       
    }

    void BANG()
    {
        Enemy.instance.TakeDamageenemy(playerStats.damage);       
    }
}
