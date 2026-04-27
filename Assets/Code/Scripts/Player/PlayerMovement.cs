using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float groundDistance = 1f;
    [SerializeField] private float groundOffset = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 lastInputDirection = Vector2.zero;
    public bool canMove = true;

    private ZiplineController zipline;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private InputSystem_Actions inputs;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
        inputs.Player.Jump.started += OnJump;
        inputs.Enable();

        rb = GetComponent<Rigidbody2D>();
        zipline = GetComponent<ZiplineController>();
        if (zipline == null) zipline = gameObject.AddComponent<ZiplineController>();
        
        zipline.Initialize(rb, this);

        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameManager.Instance.isDead)
        {
            if (anim != null) anim.SetBool("isDead", true);
            rb.simulated = false;
        }
    }

    private void FixedUpdate()
    {
        if (ShouldBlockInput()) return;
        
        if (zipline == null || !zipline.IsZiplining)
        {
            HandleHorizontalMovement();
        }
    }

    private void Update()
    {
        if (ShouldBlockInput()) return;

        if (zipline != null)
        {
            zipline.HandleZipline(IsAiming(), lastInputDirection, IsTouchingFloor());
        }
        UpdateAnimations();
    }

    private void LateUpdate()
    {
        if (ShouldBlockInput()) return;
        if (zipline != null) zipline.UpdateVisuals(IsAiming(), lastInputDirection);
    }

    private void HandleHorizontalMovement()
    {
        movementInput = inputs.Player.Move.ReadValue<Vector2>().normalized;
        if (movementInput.sqrMagnitude > 0.01f) 
        {
            lastInputDirection = movementInput;
        }
        
        if (IsAiming()) 
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (ShouldBlockInput()) return;
        if ((zipline == null || !zipline.IsZiplining) && IsTouchingFloor())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
        }
    }

    public bool IsTouchingFloor()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * groundOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundDistance, groundLayer);
        
        if (hit.collider != null)
        {
            TemporalGround tempGround = hit.collider.GetComponent<TemporalGround>();
            if (tempGround != null)
            {
                tempGround.OnDissapearGround();
            }
        }
        return hit.collider != null;
    }

    private void UpdateAnimations()
    {
        if (anim == null || spriteRenderer == null) return;

        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        bool grounded = IsTouchingFloor();

        anim.SetFloat("speed", horizontalSpeed);
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("isDead", GameManager.Instance.isDead);

        if (movementInput.x > 0.01f) spriteRenderer.flipX = false;
        else if (movementInput.x < -0.01f) spriteRenderer.flipX = true;
    }

    public bool IsAiming() => inputs.Player.Zipline.ReadValue<float>() > 0.9f;

    private bool ShouldBlockInput()
    {
        return GameManager.Instance.isDead || !GameManager.Instance.canPlay || !canMove;
    }
}
