using UnityEngine;

public class Teleporte_Checkpoint : MonoBehaviour
{
    Troca_Personagens troca;
    public Transform Spawn;
    public BoxCollider2D enemy_Collider;
    void Start()
    {
        troca = FindFirstObjectByType(typeof(Troca_Personagens)) as Troca_Personagens;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (troca != null && troca.Personagens_Object != null)
            {
                troca.Personagens_Object.transform.position = Spawn.position;
            }
        }
    }   
}
