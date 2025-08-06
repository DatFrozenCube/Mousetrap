using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputSystem_Actions inputActions;
    private bool isInputPaused;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        PauseInput();

        LevelController.LevelActions += PauseInput;
    }

    private void FixedUpdate()
    {
        if (!isInputPaused)
        {
            Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
            rb.linearVelocityX = inputVector.x * moveSpeed;
            rb.linearVelocityY = inputVector.y * moveSpeed;
        }

        else
        {
            rb.linearVelocityX = 0;
            rb.linearVelocityY = 0;
        }
    }

    private void PauseInput()
    {
        StartCoroutine(PausePlayerInput(1));
    }

    private IEnumerator PausePlayerInput(int seconds)
    {
        isInputPaused = true;
        yield return new WaitForSeconds(seconds);
        isInputPaused = false;
    }
}
