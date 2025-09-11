using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform destino;
    [SerializeField] private UnityEvent onTeleport;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = destino.position;
            onTeleport?.Invoke();
        }
    }
}
