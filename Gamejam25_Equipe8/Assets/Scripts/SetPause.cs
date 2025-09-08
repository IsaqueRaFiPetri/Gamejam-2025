using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SetPause : MonoBehaviour
{
    [SerializeField] UnityEvent OnPause, OnUnpause;
    bool isPaused;

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
