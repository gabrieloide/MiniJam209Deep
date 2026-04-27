using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private int currentLevelIndex = 0;
    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        if (levels.Count > 0 && CameraController.Instance != null)
        {
            CameraController.Instance.SnapToPosition(levels[currentLevelIndex].CenterTransform);
        }
    }

    public void NextLevel()
    {
        if (isTransitioning) return;
        if (currentLevelIndex + 1 < levels.Count)
        {
            isTransitioning = true;
            SetPlayerMovement(false);
            Time.timeScale = 0f;
            currentLevelIndex++;
            CameraController.Instance.MoveToPosition(levels[currentLevelIndex].CenterTransform, EndTransition);
        }
        else
        {
            Debug.Log("No more levels!");
        }
    }

    private void EndTransition()
    {
        isTransitioning = false;
        SetPlayerMovement(true);
        Time.timeScale = 1f;
    }

    private void SetPlayerMovement(bool enabled)
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player != null)
        {
            player.canMove = enabled;
            if (!enabled)
            {
                // Optionally freeze physics
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (rb != null) rb.linearVelocity = Vector2.zero;
            }
        }
    }

    public void PreviousLevel()
    {
        if (isTransitioning) return;
        if (currentLevelIndex - 1 >= 0)
        {
            isTransitioning = true;
            SetPlayerMovement(false);
            Time.timeScale = 0f;
            currentLevelIndex--;
            CameraController.Instance.MoveToPosition(levels[currentLevelIndex].CenterTransform, EndTransition);
        }
    }

    [ContextMenu("Auto Find Levels")]
    private void AutoFindLevels()
    {
        levels.Clear();
        levels.AddRange(FindObjectsByType<Level>(FindObjectsSortMode.InstanceID));
        // You might want to sort them by position instead of InstanceID
        levels.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y)); // Assuming top-to-bottom
    }
}
