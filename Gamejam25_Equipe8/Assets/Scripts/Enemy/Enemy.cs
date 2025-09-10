using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public CharacterStatus enemyStatus;

    private void Start()
    {
        enemyStatus.life = enemyStatus.maxLife;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D player_Collider)
    {
        if (player_Collider.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        PlayerStatus.instance.TakeDamageplayer(enemyStatus.damage);
    }

    public void TakeDamageenemy(float cost)
    {
        enemyStatus.life -= cost;
        if (enemyStatus.life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
