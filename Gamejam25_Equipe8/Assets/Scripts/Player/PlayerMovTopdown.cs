using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovTopdown : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStatus;
    Rigidbody2D rb;
    Vector2 movementInput;

    public AudioClip[] sonsDePassos;
    AudioSource audioSource;

    Coroutine passosCoroutine;
    bool isMoving = false;
    int passoIndex = 0;

    private Animator currentAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        UpdateAnimator();
    }

    void Update()
    {
        if (currentAnimator == null) UpdateAnimator();

        if (movementInput != Vector2.zero)
        {
            currentAnimator.SetFloat("Horizontal", movementInput.x);
            currentAnimator.SetFloat("Vertical", movementInput.y);
            currentAnimator.SetFloat("Speed", movementInput.sqrMagnitude);
        }
        else
        {
            currentAnimator.SetFloat("Speed", 0);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        bool currentlyMoving = movementInput != Vector2.zero;

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
                audioSource.volume = 0.3f;
                audioSource.PlayOneShot(sonsDePassos[passoIndex]);

                passoIndex++;
                if (passoIndex >= sonsDePassos.Length)
                    passoIndex = 0;
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void FixedUpdate()
    {
        float speed = playerStatus.moveSpeed;

        if (Troca_Personagens.instance.isFear)
            rb.linearVelocity = movementInput * speed * 2.5f;
        else if (Troca_Personagens.instance.isBrave)
            rb.linearVelocity = movementInput * speed * 0.5f;
        else
            rb.linearVelocity = movementInput * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            Troca_Personagens.instance.Swap(Emotions.Fear, true);
        }
    }

    public void UpdateAnimator()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                currentAnimator = child.GetComponent<Animator>();
                break;
            }
        }
    }
}
