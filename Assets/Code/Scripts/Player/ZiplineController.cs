using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ZiplineController : MonoBehaviour
{
    [SerializeField] private float ziplineSpeed = 30f;
    [SerializeField] private float hookLaunchSpeed = 100f;
    [SerializeField] private float minZiplineDistance = 1.5f;
    [SerializeField] private LayerMask ziplineLayer;
    [SerializeField] private SpriteRenderer chainRenderer;
    [SerializeField] private float chainThickness = 0.1f;
    [SerializeField] private Transform hookTransform;

    public bool IsZiplining { get; private set; } = false;
    private bool canZiplineInAir = true;
    private Vector2 currentTargetPoint;
    private Vector2 hookVisualPos;
    private bool isHookTraveling = false;
    private IEnumerator ziplineRoutine;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    public void Initialize(Rigidbody2D playerRb, PlayerMovement movement)
    {
        rb = playerRb;
        playerMovement = movement;
    }

    public void HandleZipline(bool isPressing, Vector2 aimDirection, bool isGrounded)
    {
        if (isGrounded) canZiplineInAir = true;

        if (isPressing && !IsZiplining && canZiplineInAir)
        {
            RaycastHit2D hit = FindAnchor(aimDirection);
            if (hit.collider != null)
            {
                float dist = Vector2.Distance(transform.position, hit.point);
                if (dist > minZiplineDistance)
                {
                    StartZipline(hit.point + (hit.normal * 0.5f), isGrounded);
                }
            }
        }
        else if (!isPressing && IsZiplining)
        {
            StopZipline();
        }
    }

    public void UpdateVisuals(bool isAiming, Vector2 aimDirection)
    {
        bool shouldShow = IsZiplining || isAiming;

        if (shouldShow)
        {
            ToggleVisuals(true);
            if (IsZiplining)
            {
                hookTransform.position = isHookTraveling ? hookVisualPos : currentTargetPoint;
            }
            else
            {
                RaycastHit2D hit = FindAnchor(aimDirection);
                if (hit.collider != null)
                {
                    hookTransform.position = hit.point + (hit.normal * 0.5f);
                    currentTargetPoint = hookTransform.position;
                }
                else
                {
                    ToggleVisuals(false);
                }
            }
            if (chainRenderer != null && chainRenderer.enabled) UpdateChainRotation();
        }
        else
        {
            ToggleVisuals(false);
        }
    }

    private void StartZipline(Vector2 target, bool isGrounded)
    {
        IsZiplining = true;
        if (!isGrounded) canZiplineInAir = false;
        if (ziplineRoutine != null) StopCoroutine(ziplineRoutine);
        ziplineRoutine = ZiplineMovementRoutine(target);
        StartCoroutine(ziplineRoutine);
    }

    public void StopZipline()
    {
        IsZiplining = false;
        if (ziplineRoutine != null) StopCoroutine(ziplineRoutine);
        if (rb != null) rb.gravityScale = 3f;
    }

    private IEnumerator ZiplineMovementRoutine(Vector2 targetPosition)
    {
        currentTargetPoint = targetPosition;
        hookVisualPos = transform.position;
        isHookTraveling = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.ziplineShoot);

        while (Vector2.Distance(hookVisualPos, targetPosition) > 0.2f)
        {
            hookVisualPos = Vector2.MoveTowards(hookVisualPos, targetPosition, hookLaunchSpeed * Time.deltaTime);
            yield return null;
        }

        hookVisualPos = targetPosition;
        isHookTraveling = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.ziplineHit);
        if (Camera.main != null) Camera.main.transform.DOShakePosition(0.15f, 0.2f, 20, 90, false, true);

        while (IsZiplining)
        {
            float distance = Vector2.Distance(transform.position, targetPosition);
            if (distance < 0.5f)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0;
                transform.position = targetPosition;
            }
            else
            {
                rb.gravityScale = 0;
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                rb.linearVelocity = direction * ziplineSpeed;
            }
            yield return null;
        }
        rb.gravityScale = 3f;
    }

    private RaycastHit2D FindAnchor(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return new RaycastHit2D();
        return Physics2D.Raycast(transform.position, direction, Mathf.Infinity, ziplineLayer);
    }

    private void UpdateChainRotation()
    {
        Vector2 dir = (Vector2)hookTransform.position - (Vector2)transform.position;
        float distance = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        chainRenderer.transform.position = (transform.position + hookTransform.position) / 2f;
        chainRenderer.transform.rotation = Quaternion.Euler(0, 0, angle);
        chainRenderer.transform.localScale = new Vector3(distance, chainThickness, 1f);
        hookTransform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    private void ToggleVisuals(bool state)
    {
        if (hookTransform != null) hookTransform.gameObject.SetActive(state);
        if (chainRenderer != null) chainRenderer.enabled = state;
    }
}
