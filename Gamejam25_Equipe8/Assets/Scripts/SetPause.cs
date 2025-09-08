using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SetPause : MonoBehaviour
{
    [SerializeField] UnityEvent OnPause, OnUnpause;
    bool isPaused;

    PlayerInputActions playerActionsInput;

    private void Awake()
    {
        playerActionsInput = new PlayerInputActions();
    }

    void OnEnable()
    {
        // Enable the action map
        playerActionsInput.Enable();

        // Subscribe to Pause action
        playerActionsInput.Player.Pause.performed += Pause;
        playerActionsInput.Player.Pause.canceled += Pause;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid leaks
        playerActionsInput.Player.Pause.performed -= Pause;
        playerActionsInput.Player.Pause.canceled -= Pause;

        playerActionsInput.Disable();
    }

    void SetCursor()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = isPaused;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPause.Invoke();
            SetPaused(true);
            SetCursor();
        }
        if (context.canceled)
        {
            OnUnpause.Invoke();
            SetPaused(false);
            SetCursor();
        }
    }

    bool SetPaused(bool _paused)
    {
        return isPaused = _paused;
    }
}
