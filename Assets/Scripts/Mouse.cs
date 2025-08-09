using System.Collections;
using MoreMountains.Tools;
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
    private Animator animator;

    [SerializeField] private AudioClip squeezeSfx;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private int squeezeTime = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spawner = GameObject.FindGameObjectWithTag("Creator").GetComponent<MazeSpawner>();
        animator = GetComponentInChildren<Animator>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Squeeze.performed += OnSqueeze;
        inputActions.Player.Squeeze.canceled += OnSqueeze;

        PauseInput();
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
            animator.SetBool("Shrink", false);
        }
    }

    private void OnSqueeze(InputAction.CallbackContext context)
    {
        if (!isInputPaused)
        {
            if (context.performed)
            {
                StartCoroutine(StartSqueeze(squeezeTime));
            }

            else if (context.canceled)
            {
                StopCoroutine(StartSqueeze(squeezeTime));
                animator.SetBool("Shrink", false);
                MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Stop, 5);
            }
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

    private IEnumerator StartSqueeze(int seconds)
    {
        animator.SetBool("Shrink", true);
        MMSoundManagerSoundPlayEvent.Trigger(squeezeSfx, MMSoundManager.MMSoundManagerTracks.Sfx, transform.position, ID: 5);
        yield return new WaitForSeconds(seconds);
        animator.SetBool("Shrink", false);
        MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Stop, 5);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GoalDetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TrapDetectionRadius);
    }
}
