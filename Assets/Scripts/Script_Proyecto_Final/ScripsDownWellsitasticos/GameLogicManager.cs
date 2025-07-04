using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance { get; private set; }

    private int initialLives = 3; // Guardamos el n�mero de vidas inicial
    private int lives;
    private bool isPaused = false;

    private UIManager uiManager;
    private Gallina_Move player;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Al crearse por primera vez, establece las vidas
        lives = initialLives;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        isPaused = false;

        CheckForEventSystem();

        player = FindObjectOfType<Gallina_Move>();
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager == null)
        {
            Debug.LogError("�CR�TICO! No se encontr� un UIManager en la escena cargada.");
            return;
        }

        uiManager.SetupInitialUI(lives);
        if (player != null)
        {
            uiManager.UpdateHealthText(player.currentHealth);
        }
    }

    // ... (El m�todo CheckForEventSystem y Update se quedan igual)
    private void CheckForEventSystem() { /* ... */ }
    void Update() { /* ... */ }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        uiManager?.ShowPausePanel(isPaused);
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Esta funci�n es para reiniciar el nivel (por ejemplo, desde el men� de pausa)
    // SIN restaurar las vidas.
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- �NUEVA FUNCI�N! ---
    // Esta funci�n es para el bot�n de Game Over. Restaura las vidas Y reinicia el nivel.
    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego desde cero. Vidas restauradas a " + initialLives);
        // 1. Restaura la variable de vidas al valor inicial.
        lives = initialLives;

        // 2. Llama a la funci�n de reinicio de nivel.
        RestartLevel();
    }
    // ----------------------

    public void PlayerHasDied()
    {
        lives--;
        uiManager?.UpdateLivesText(lives);

        if (lives > 0)
        {
            // Si te quedan vidas, solo se reinicia el nivel.
            Invoke("RestartLevel", 1.5f);
        }
        else
        {
            // Si no te quedan vidas, se muestra la pantalla de Game Over.
            uiManager?.ShowGameOverPanel();
            Time.timeScale = 0f;
        }
    }

    public void OnPlayerHealthChanged(int newHealth)
    {
        uiManager?.UpdateHealthText(newHealth);
    }
}

