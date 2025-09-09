using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStats;    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BANG();
            print("tiro");
        }
        print("detectado");
    }

    void BANG()
    {
        Enemy.instance.TakeDamageenemy(playerStats.damage);
        print("WHAT");
    }
}
