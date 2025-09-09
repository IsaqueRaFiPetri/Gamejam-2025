using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;  
    public float moveSpeed = 3f;       

    void Update()
    {
        if (playerTransform != null)
        {           
            Vector2 direction = (playerTransform.position - transform.position).normalized;
           
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }
}
