using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovTopdown : MonoBehaviour
{
    [SerializeField] CharacterStatus playerStatus;

    Vector2 movementInput;

    PlayerInputActions playerActionsInput;

    Troca_Personagens troca;

    private void Start()
    {
        troca = FindFirstObjectByType(typeof(Troca_Personagens)) as Troca_Personagens; ; 
    }
    private void Awake()
    {
        playerActionsInput = new PlayerInputActions();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        if (Troca_Personagens.instance.isFear)
        {
            Vector3 newPos = transform.position + new Vector3(movementInput.x, movementInput.y, 0) * playerStatus.moveSpeed * 3 * Time.deltaTime;
            transform.position = newPos;

            playerActionsInput.Player.Enable();
            playerActionsInput.Player.Move.performed += OnMove;
            playerActionsInput.Player.Move.canceled += OnMove;
        }
        else if (Troca_Personagens.instance.isBrave)
        {
            Vector3 newPos = transform.position + new Vector3(movementInput.x, movementInput.y, 0) * playerStatus.moveSpeed / 2 * Time.deltaTime;
            transform.position = newPos;

            playerActionsInput.Player.Enable();
            playerActionsInput.Player.Move.performed += OnMove;
            playerActionsInput.Player.Move.canceled += OnMove;
        }
        else
        {
            Vector3 newPos = transform.position + new Vector3(movementInput.x, movementInput.y, 0) * playerStatus.moveSpeed * Time.deltaTime;
            transform.position = newPos;

            playerActionsInput.Player.Enable();
            playerActionsInput.Player.Move.performed += OnMove;
            playerActionsInput.Player.Move.canceled += OnMove;
        }
    }
}