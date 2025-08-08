using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public float TrapDetectionRadius = 2f;
    public float GoalDetectionRadius = 4f;
    //Used to detect if there are any traps or goals nearby

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputSystem_Actions inputActions;
    private bool isInputPaused;
    private MazeSpawner spawner;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spawner = GameObject.FindGameObjectWithTag("Creator").GetComponent<MazeSpawner>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        PauseInput();
        //ResetPlayerPosition();
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

    public void PauseInput()
    {
        StartCoroutine(PausePlayerInput(1));
    }

    public void ResetPlayerPosition()
    {
        transform.position = new Vector3((spawner.Rows * spawner.CellWidth) / 2, (spawner.Columns * spawner.CellHeight) / 2, 0);
    }

    private IEnumerator PausePlayerInput(int seconds)
    {
        isInputPaused = true;
        yield return new WaitForSeconds(seconds);
        isInputPaused = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GoalDetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TrapDetectionRadius);
    }
}
