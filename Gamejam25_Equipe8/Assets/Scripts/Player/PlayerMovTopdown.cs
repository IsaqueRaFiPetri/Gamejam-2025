using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovTopdown : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

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
        //MedoMode();
        Vector3 newPos = transform.position + new Vector3(movementInput.x, movementInput.y, 0) * moveSpeed * Time.deltaTime;
        transform.position = newPos;

        playerActionsInput.Player.Enable();
        playerActionsInput.Player.Move.performed += OnMove;
        playerActionsInput.Player.Move.canceled += OnMove;
    }

    //void MedoMode()
   // {
       // if (troca.Medo_Object == true)
       // {
        //    moveSpeed = 10f;
      //  }
       // else if(troca.Medo_Object == false)
       // {
       //     moveSpeed = 5f;
       // }
    //}
}
//
