using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;


public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public CharacterStatus enemyStatus;
    public Animator animator; // Animator do inimigo
                              // public Animator attackanim;
    private Rigidbody2D rb;

    private bool canMove = false; // só anda após animação inicial
    private Vector2 moveDirection;
    private float lastAttackTime = 0f;

    public AudioClip soco;
    AudioSource audioPunch;
    AudioSource audioPassos;
    public AudioClip[] sonsDePassos;
    [SerializeField] Image lifeBar;
    Coroutine passosCoroutine;
    bool isMoving = false;
    int passoIndex = 0;

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
        GameObject punchAudioObj = new GameObject("AudioPunch");
        punchAudioObj.transform.parent = transform;
        audioPunch = punchAudioObj.AddComponent<AudioSource>();
        audioPunch.playOnAwake = false;
        audioPunch.loop = false;
        audioPunch.volume = 9999992f; // volume do soco

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

        lifeBar.fillAmount = enemyStatus.life / enemyStatus.maxLife;
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
                {
                    //attackanim?.SetTrigger("Attack");
                    audioPunch.PlayOneShot(soco);
                }
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

                passoIndex++;
                if (passoIndex >= sonsDePassos.Length)
                    passoIndex = 0;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}

