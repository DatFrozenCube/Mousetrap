using System.Collections;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public float TrapDetectionRadius = 2f;
    public float GoalDetectionRadius = 4f;
    public float moveSpeed = 10f;
    //Used to detect if there are any traps or goals nearby

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputSystem_Actions inputActions;
    private bool isInputPaused;
    //private MazeSpawner spawner;
    private Animator animator;
    private SpriteRenderer mouseSprite;
    private Health health;
    private CinemachineImpulseSource impulseSource;
    private MMF_Player mmfPlayer;

    [SerializeField] private AudioClip squeezeSfx;
    [SerializeField] private int squeezeTime = 3;

    private void Start()
    {
        Cheese.CheeseActions += PauseInput;
        PauseManager.pauseActions += PauseInput;
        PauseManager.resumeActions += UnpauseInput;
    }

    private void OnDestroy()
    {
        Cheese.CheeseActions -= PauseInput;
        PauseManager.pauseActions -= PauseInput;
        PauseManager.resumeActions -= UnpauseInput;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        //spawner = GameObject.FindGameObjectWithTag("Creator").GetComponent<MazeSpawner>();
        animator = GetComponentInChildren<Animator>();
        mouseSprite = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        mmfPlayer = GetComponent<MMF_Player>();
        //mouseSprite.material.color = Random.ColorHSV();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Shop.performed += Shop;
        inputActions.Player.Shop.canceled += Shop;
        //inputActions.Player.Squeeze.performed += OnSqueeze;
        //inputActions.Player.Squeeze.canceled += OnSqueeze;

        PauseInputForSeconds(1);
    }

    private void FixedUpdate()
    {
        if (!isInputPaused)
        {
            Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
            float moveSpeedX = inputVector.x * moveSpeed;
            float moveSpeedY = inputVector.y * moveSpeed;

            rb.linearVelocityX = moveSpeedX;
            rb.linearVelocityY = moveSpeedY;

            if (Mathf.Abs(moveSpeedX) > 0.1f)
            {
                mouseSprite.flipX = moveSpeedX < 0;
                animator.SetInteger("FrontBackSide", 2);
                animator.SetFloat("Speed", Mathf.Abs(moveSpeedX));
            }

            else if (Mathf.Abs(moveSpeedY) > 0.1f)
            {
                animator.SetInteger("FrontBackSide", moveSpeedY < 0 ? 0 : 1);
                animator.SetFloat("Speed", Mathf.Abs(moveSpeedY));
            }

            else
            {
                animator.SetFloat("Speed", 0);
            }
        }

        else
        {
            rb.linearVelocityX = 0;
            rb.linearVelocityY = 0;
            animator.SetBool("Shrink", false);
        }
    }
    
    public bool SpawnPlayer(Vector2 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            return true;
        }

        return false;
    }

    private void Shop(InputAction.CallbackContext context)
    {
        if (PauseManager.pauseAvailable)
        {
            if (context.performed)
            {
                ShopManager.Instance.ToggleShop();
            }
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
        isInputPaused = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Animator>().StartPlayback();
    }

    public void UnpauseInput()
    {
        isInputPaused = false;
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<Animator>().StopPlayback();
    }

    public void PauseInputForSeconds(int seconds)
    {
        StartCoroutine(PausePlayerInput(seconds));
    }

    /*
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3((spawner.Rows * spawner.CellWidth) / 2, (spawner.Columns * spawner.CellHeight) / 2, 0);
    }
    */

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
