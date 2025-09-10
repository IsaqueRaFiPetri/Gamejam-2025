using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2f;
    public CharacterStatus enemyStatus;
    public Animator animator; // Animator do inimigo
    private Rigidbody2D rb;

    private bool canMove = false; // só anda após animação inicial
    private Vector2 moveDirection;
    private float lastAttackTime = 0f;

    [SerializeField] Image lifeBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyStatus.life = enemyStatus.maxLife;

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null && canMove)
        {
            moveDirection = (playerTransform.position - transform.position).normalized;
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        lifeBar.fillAmount = enemyStatus.life / enemyStatus.maxLife;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + enemyStatus.cooldown)
            {
                PlayerStatus.instance.TakeDamageplayer(enemyStatus.damage);
                lastAttackTime = Time.time;
            }
        }

    }

    public void TakeDamageenemy(float cost)
    {
        enemyStatus.life -= cost / enemyStatus.toughness;
        
        if (enemyStatus.life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void StartMoving()
    {
        canMove = true;
        animator.SetBool("hasSwitched", true);
    }
}
