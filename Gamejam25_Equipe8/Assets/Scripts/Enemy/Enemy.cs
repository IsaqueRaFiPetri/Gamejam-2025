using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public CharacterStatus enemyStatus;
    public Transform playerTransform;
    public float moveSpeed = 3f;
    public BoxCollider2D player_Collider;
    private bool canDetectCollision = true;
    public CharacterStatus EnemyStatus;

    void Start()
    {
        EnemyStatus.life = EnemyStatus.maxLife;        
    }
    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionStay2D(Collision2D player_Collider)
    {
        if(player_Collider.gameObject.CompareTag("Player") && canDetectCollision)
        {
            Debug.Log("Inimigo colidiu com o player!");
            Attack();
            canDetectCollision = false;
            StartCoroutine(ResetCollisionDetection());
        }
    }
    IEnumerator ResetCollisionDetection()
    {
        yield return new WaitForSeconds(1f);
        canDetectCollision = true;
    }

    void Attack()
    {
        PlayerStatus.instance.TakeDamage(enemyStatus.damage);
    }
}
