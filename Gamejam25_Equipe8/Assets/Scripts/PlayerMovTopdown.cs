using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovTopdown : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Vector2 movementInput;

    PlayerInputActions playerActionsInput;

    private void Awake()
    {
        playerActionsInput = new PlayerInputActions();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log($"Move input: {movementInput}");

    }

    private void FixedUpdate()
    {
        Vector3 newPos = transform.position + new Vector3(movementInput.x, movementInput.y, 0) * moveSpeed * Time.deltaTime;
        transform.position = newPos;
        Debug.Log($"Transform moved to: {newPos}");

        playerActionsInput.Player.Enable();
        playerActionsInput.Player.Move.performed += OnMove;
        playerActionsInput.Player.Move.canceled += OnMove;
    }
}
