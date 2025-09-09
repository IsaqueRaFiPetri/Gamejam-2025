using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 3f;
    public BoxCollider2D player_Collider;

    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D player_Collider)
    {
        if (player_Collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inimigo colidiu com o player!");
        }
    }
}
