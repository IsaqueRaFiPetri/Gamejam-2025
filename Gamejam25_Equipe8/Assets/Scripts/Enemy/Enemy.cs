using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    [SerializeField] Image lifeBar;
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public BoxCollider2D player_Collider;
    private bool canDetectCollision = true;
    public CharacterStatus enemyStatus;

    void Start()
    {
        enemyStatus.life = enemyStatus.maxLife;
    }
    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
        lifeBar.fillAmount = enemyStatus.life / enemyStatus.maxLife;
    }

    void OnCollisionStay2D(Collision2D player_Collider)
    {
        if (player_Collider.gameObject.CompareTag("Player") && canDetectCollision)
        {
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
        if (!Troca_Personagens.instance.isSad)
        {
            PlayerStatus.instance.TakeDamageplayer(enemyStatus.damage);
        }        
    }
    public void TakeDamageenemy(float damage)
    {
        if (enemyStatus.life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
