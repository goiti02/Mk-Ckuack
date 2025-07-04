using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

// La "ficha" de datos del nivel.
[System.Serializable]
public class LevelData_SpaceInvaders
{
    [Header("Velocidades")]
    public float horizontalSpeed = 44f;
    public float verticalSpeed = 18f;
    public float enemyBulletSpeed = 140f;
    public float enemySpeedIncrease = 4f;
    [Header("Disparos de los Enemigos")]
    public float enemyFireFrequency = 3.5f;
    [Header("Formación de Enemigos")]
    public int columns = 5;
    public int rows = 5;
    [Header("Tipos de Enemigos")]
    public GameObject[] enemyPrefabs;
}

// Los estados del juego.
public enum GameState { Playing, Paused, GameOver }

public class GameManager_SpaceInvaders : MonoBehaviour
{
    [Header("Datos del Nivel")]
    public LevelData_SpaceInvaders levelData;
    [Header("Prefabs")]
    public GameObject bonusShipPrefab;
    [Header("Paneles UI")]
    public GameObject gameplayHudPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    [Header("Textos UI")]
    public TextMeshProUGUI resultTitleText;
    public TextMeshProUGUI finalMessageText;
    public TextMeshProUGUI countdownText;
    [Header("Managers")]
    public PlayerHUD_SpaceInvaders playerHud;
    public EnemyManager_SpaceInvaders enemyManager;

    public GameState CurrentState { get; private set; }
    private int score;
    private float bonusShipTimer;

    void Start()
    {
        // El juego empieza solo al cargar la escena.
        Time.timeScale = 1f;
        score = 0;
        if (playerHud != null) playerHud.UpdateScore(score);
        StartCoroutine(CountdownSequence());
    }

    void Update()
    {
        if (CurrentState == GameState.Playing && Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }

        if (CurrentState == GameState.Playing)
        {
            bonusShipTimer -= Time.deltaTime;
            if (bonusShipTimer <= 0)
            {
                SpawnBonusShip();
            }
        }
    }

    public void TogglePause()
    {
        bool isPaused = !pausePanel.activeSelf;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        CurrentState = isPaused ? GameState.Paused : GameState.Playing;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (playerHud != null) playerHud.UpdateScore(score);
    }

    public void TriggerGameOver(bool didWin)
    {
        if (CurrentState == GameState.Playing)
        {
            CurrentState = GameState.GameOver;
            StartCoroutine(GameOverSequence(didWin));
        }
    }

    private void SpawnBonusShip()
    {
        if (bonusShipPrefab == null) return;
        Instantiate(bonusShipPrefab, new Vector3(-20f, 18f, 0f), Quaternion.identity);
        bonusShipTimer = Random.Range(15f, 30f);
    }

    private IEnumerator CountdownSequence()
    {
        CurrentState = GameState.Paused;
        bonusShipTimer = Random.Range(10f, 20f);

        if (gameplayHudPanel != null) gameplayHudPanel.SetActive(true);
        if (countdownText != null) countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        if (countdownText != null) countdownText.gameObject.SetActive(false);

        CurrentState = GameState.Playing;
        if (enemyManager != null) enemyManager.StartWave(levelData);
    }

    private IEnumerator GameOverSequence(bool didWin)
    {
        if (gameplayHudPanel != null) gameplayHudPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        if (didWin) { resultTitleText.text = "¡VICTORIA!"; }
        else { resultTitleText.text = "GAME OVER"; }
        finalMessageText.text = "Pulsa ESPACIO para reiniciar";

        yield return new WaitForSeconds(0.5f); // Pequeña pausa antes de aceptar input.

        while (!Input.GetButtonDown("Fire"))
        {
            yield return null;
        }

        RestartGame();
    }
}