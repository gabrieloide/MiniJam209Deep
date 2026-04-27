using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Transform pivot;
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private Ease transitionEase = Ease.InOutQuad;
    
    private CinemachineCamera virtualCamera;
    private Tween currentTween;
    private Transform currentTarget;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        virtualCamera = GetComponent<CinemachineCamera>();
    }

    public void SnapToPosition(Transform targetTransform)
    {
        if (pivot == null)
        {
            Debug.LogWarning("No Pivot assigned to CameraController!");
            return;
        }

        // Snap the pivot to the target position
        pivot.position = targetTransform.position;
        
        // Ensure virtual camera is looking at the pivot immediately
        if (virtualCamera != null)
        {
            virtualCamera.Follow = pivot;
            virtualCamera.ForceCameraPosition(virtualCamera.transform.position, virtualCamera.transform.rotation);
        }
    }

    public void MoveToPosition(Transform targetTransform, System.Action onComplete = null)
    {
        if (pivot == null)
        {
            Debug.LogWarning("No Pivot assigned to CameraController!");
            return;
        }

        // Prevent duplicate calls to the same target
        if (currentTarget == targetTransform) return;

        // Stop any current transition
        currentTween?.Kill();
        currentTarget = targetTransform;

        // Move the PIVOT instead of the camera
        // Cinemachine will follow the pivot based on its own settings (Body damping, etc.)
        currentTween = pivot.DOMove(targetTransform.position, transitionDuration)
            .SetEase(transitionEase)
            .SetUpdate(true) // Crucial: ignore Time.timeScale = 0
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                currentTween = null;
                currentTarget = null;
            });
    }
}
