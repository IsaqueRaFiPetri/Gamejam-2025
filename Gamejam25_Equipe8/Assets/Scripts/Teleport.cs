using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform destino; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = destino.position; 
        }
    }
}
