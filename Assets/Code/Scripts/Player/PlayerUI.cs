using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private UIPanel gameOverPanel;
    [SerializeField] private UIPanel gameHUDPanel;
    [SerializeField] private UIPanel pausePanel;
    [SerializeField] private UIPanel victoryPanel;
    [SerializeField] private UIPanel panelStartAlive;
    [SerializeField] private UIPanel panelStartDead;
    [SerializeField] private TMP_Text depthText;
    [SerializeField] private TMP_Text victoryStatsText;
    [SerializeField] private TMP_InputField playerNameInputField;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        InitializeUI();
        SubscribeToEvents();
    }

    private void Update()
    {
        if (GameManager.Instance.isDead) return;
        depthText.text = "Depth: " + Mathf.Abs(GameManager.Instance.depth).ToString("F0") + "m";
    }

    private void OnDestroy() => UnsubscribeFromEvents();

    private void InitializeUI()
    {
        if (GameManager.Instance.isDead)
        {
            panelStartAlive.gameObject.SetActive(false);
            panelStartDead.Show();
        }
        else
        {
            panelStartDead.gameObject.SetActive(false);
            panelStartAlive.Show();
        }
        playerNameInputField.onSubmit.AddListener(OnNameSubmit);
    }

    private void OnNameSubmit(string name)
    {
        if (name.Length >= 3)
        {
            GameManager.Instance.playerName = name;
            panelStartAlive.Hide(() => gameHUDPanel.Show());
            GameManager.Instance.OnPlayerNameSubmit(name.Length);
        }
    }

    private void SubscribeToEvents()
    {
        GameManager.OnDeathAction += OnDeath;
        GameManager.OnVictoryAction += OnVictory;
        GameManager.OnPauseAction += OnPause;
        GameManager.OnResumeAction += OnResume;
    }

    private void UnsubscribeFromEvents()
    {
        GameManager.OnDeathAction -= OnDeath;
        GameManager.OnVictoryAction -= OnVictory;
        GameManager.OnPauseAction -= OnPause;
        GameManager.OnResumeAction -= OnResume;
    }

    private void OnVictory()
    {
        victoryPanel.Show();
        float timeTaken = Time.time - startTime;
        victoryStatsText.text = $"Depth: {Mathf.Abs(GameManager.Instance.depth):F0}m\nTime: {timeTaken:F1}s";
        gameHUDPanel.gameObject.SetActive(false);
    }

    private void OnDeath(Vector2 pos, string hazard, string pName)
    {
        gameOverPanel.Show();
        gameHUDPanel.gameObject.SetActive(false);
    }

    private void OnPause()
    {
        pausePanel.Show();
        gameHUDPanel.gameObject.SetActive(false);
    }

    private void OnResume()
    {
        pausePanel.Hide(() => gameHUDPanel.Show());
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("gameOver", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}