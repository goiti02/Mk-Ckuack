using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener esta línea para poder usar 'Button'.
using TMPro;

public class UIManager : MonoBehaviour
{
    // Ya no hay [SerializeField] para los botones. El script los encontrará solo.
    [Header("Referencias a Elementos de la UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private Button continueButton;
    private Button restartButtonInPause;
    private Button restartButtonInGameOver;
    private Button screenPauseButton;

    private void Awake()
    {
        Debug.Log("[UIManager] Awake: Buscando elementos de la UI y conectando botones...");

        // Búsqueda automática de todos los elementos
        healthText = transform.Find("HealthText")?.GetComponent<TextMeshProUGUI>();
        livesText = transform.Find("LivesContainer/LivesText")?.GetComponent<TextMeshProUGUI>();
        pausePanel = transform.Find("PausePanel")?.gameObject;
        gameOverPanel = transform.Find("GameOverPanel")?.gameObject;

        continueButton = transform.Find("PausePanel/ContinueButton")?.GetComponent<Button>();
        restartButtonInPause = transform.Find("PausePanel/RestartButton")?.GetComponent<Button>();
        restartButtonInGameOver = transform.Find("GameOverPanel/RestartButton")?.GetComponent<Button>();
        screenPauseButton = transform.Find("PauseButton")?.GetComponent<Button>();

        // --- ASIGNACIÓN DE FUNCIONES A LOS BOTONES POR CÓDIGO ---

        continueButton?.onClick.AddListener(() => GameLogicManager.Instance.TogglePause());
        screenPauseButton?.onClick.AddListener(() => GameLogicManager.Instance.TogglePause());

        // El botón de reiniciar en el menú de pausa NO resetea las vidas.
        restartButtonInPause?.onClick.AddListener(() => GameLogicManager.Instance.RestartLevel());

        // El botón de reiniciar en el menú de Game Over SÍ resetea las vidas.
       
        restartButtonInGameOver?.onClick.AddListener(() => GameLogicManager.Instance.RestartGame());

        Debug.Log("[UIManager] Conexiones de botones realizadas por código.");
    }

    // El GameLogicManager llamará a estas funciones
    public void SetupInitialUI(int currentLives)
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateLivesText(currentLives);
    }

    public void UpdateHealthText(int currentHealth)
    {
        if (healthText != null) healthText.text = "VIDA: " + currentHealth.ToString();
    }

    public void UpdateLivesText(int currentLives)
    {
        if (livesText != null) livesText.text = "x " + currentLives.ToString();
    }

    public void ShowPausePanel(bool show)
    {
        if (pausePanel != null) pausePanel.SetActive(show);
    }

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}

