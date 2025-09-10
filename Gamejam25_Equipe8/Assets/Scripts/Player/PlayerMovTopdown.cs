using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovTopdown : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStatus;
    Rigidbody2D rb;
    Vector2 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (Troca_Personagens.instance.isFear)
            rb.linearVelocity = movementInput * playerStatus.moveSpeed * 5 / 2;
        else if (Troca_Personagens.instance.isBrave)
            rb.linearVelocity = movementInput * playerStatus.moveSpeed / 2;
        else
            rb.linearVelocity = movementInput * playerStatus.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            Troca_Personagens.instance.Swap(Emotions.Fear);
        }
    }
}