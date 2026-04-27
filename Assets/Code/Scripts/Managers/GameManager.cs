using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Action<Vector2, string, string> OnDeathAction;
    public static Action OnVictoryAction;
    public static Action OnPauseAction;
    public static Action OnResumeAction;

    public string playerName;
    public float depth;
    public bool isDead = false;
    public bool canPlay = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerNameSubmit(int nameLength)
    {
        if (nameLength >= 3) canPlay = true;
    }

    public void OnDeath(Vector2 deathPosition, string hazardName)
    {
        if (isDead) return;
        isDead = true;
        Time.timeScale = 0;
        PlayerPrefs.SetInt("gameOver", 1);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
        OnDeathAction?.Invoke(deathPosition, hazardName, playerName);
    }

    public void WinGame()
    {
        canPlay = false;
        Time.timeScale = 0;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.victory);
        OnVictoryAction?.Invoke();
    }
}