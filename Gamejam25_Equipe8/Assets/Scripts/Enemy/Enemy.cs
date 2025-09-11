using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Configurações")]
    public CharacterStatus enemyStatus; // ScriptableObject com dados base
    public Transform playerTransform;
    public Animator animator;

    [Header("Vida")]
    [SerializeField] private Image lifeBar;
    private float currentLife;

    [Header("Áudio")]
    public AudioClip soco;
    public AudioClip[] sonsDePassos;
    private AudioSource audioPunch;
    private AudioSource audioPassos;

    [Header("Drop de Item")]
    [SerializeField] private GameObject[] dropItems; // Array de itens que podem cair
    [Range(0f, 1f)] public float dropChance = 0.3f;  // Chance de dropar (30% por padrão)

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool canMove = false;
    private bool isMoving = false;
    private Coroutine passosCoroutine;
    private int passoIndex = 0;
    private float lastAttackTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentLife = enemyStatus.maxLife;

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        GameObject punchAudioObj = new GameObject("AudioPunch");
        punchAudioObj.transform.parent = transform;
        audioPunch = punchAudioObj.AddComponent<AudioSource>();
        audioPunch.playOnAwake = false;
        audioPunch.loop = false;
        audioPunch.volume = 0.8f;

        GameObject passosAudioObj = new GameObject("AudioPassos");
        passosAudioObj.transform.parent = transform;
        audioPassos = passosAudioObj.AddComponent<AudioSource>();
        audioPassos.playOnAwake = false;
        audioPassos.loop = false;
        audioPassos.volume = 0.15f;
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

        if (lifeBar != null)
            lifeBar.fillAmount = currentLife / enemyStatus.maxLife;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + moveDirection * enemyStatus.moveSpeed * Time.fixedDeltaTime);

            bool currentlyMoving = moveDirection != Vector2.zero;

            if (currentlyMoving && !isMoving)
            {
                isMoving = true;
                passosCoroutine = StartCoroutine(TocarPassosEmOrdem());
            }
            else if (!currentlyMoving && isMoving)
            {
                isMoving = false;
                if (passosCoroutine != null)
                    StopCoroutine(passosCoroutine);
            }
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

                if (soco != null)
                    audioPunch.PlayOneShot(soco);
            }
        }
    }

    public void TakeDamageenemy(float cost)
    {
        currentLife -= cost / enemyStatus.toughness;

        if (currentLife <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }

        if (lifeBar != null)
            lifeBar.fillAmount = currentLife / enemyStatus.maxLife;
    }

    private void DropItem()
    {
        if (dropItems.Length == 0) return;

        float roll = Random.value; // valor entre 0 e 1
        if (roll <= dropChance)
        {
            int index = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[index], transform.position, Quaternion.identity);
        }
    }

    public void StartMoving()
    {
        canMove = true;
        if (animator != null)
            animator.SetBool("hasSwitched", true);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        bool currentlyMoving = moveDirection != Vector2.zero;

        if (currentlyMoving && !isMoving)
        {
            isMoving = true;
            passosCoroutine = StartCoroutine(TocarPassosEmOrdem());
        }
        else if (!currentlyMoving && isMoving)
        {
            isMoving = false;
            if (passosCoroutine != null)
                StopCoroutine(passosCoroutine);
        }
    }

    IEnumerator TocarPassosEmOrdem()
    {
        passoIndex = 0;

        while (true)
        {
            float delay = Troca_Personagens.instance.isFear ? 0.25f : 0.465f;

            if (sonsDePassos.Length > 0)
            {
                audioPassos.PlayOneShot(sonsDePassos[passoIndex]);
                passoIndex = (passoIndex + 1) % sonsDePassos.Length;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}

