using UnityEngine;
using System.Collections.Generic; // Para usar Listas

public class EnemyManager_SpaceInvaders : MonoBehaviour
{
    [Header("Referencias")]
    public GameManager_SpaceInvaders gameManager;
    public Transform enemyContainer; // Un objeto vacío que será el padre de todos los enemigos.
    public GameObject enemyBulletPrefab; // El Prefab de la bala enemiga.

    [Header("Límites de Movimiento")]
    public Transform leftBoundary;
    public Transform rightBoundary;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private Vector3 moveDirection = Vector3.right;
    private float currentSpeed;
    private float fireTimer;
    private LevelData_SpaceInvaders levelData;

    // El GameManager llamará a esto para iniciar la oleada.
    public void StartWave(LevelData_SpaceInvaders data)
    {
        levelData = data;
        currentSpeed = levelData.horizontalSpeed;
        SpawnEnemies();
    }

    void Update()
    {
        

        // Muevo el contenedor, y con él, a todos los enemigos.
        enemyContainer.Translate(moveDirection * currentSpeed * Time.deltaTime);

        CheckBoundaries();
        HandleShooting();
    }

    private void CheckBoundaries()
    {
        foreach (var enemy in activeEnemies)
        {
            // Si un enemigo llega al borde derecho...
            if (moveDirection.x > 0 && enemy.transform.position.x >= rightBoundary.position.x)
            {
                SwitchDirectionAndMoveDown();
                break; // Solo necesito hacerlo una vez.
            }
            // Si un enemigo llega al borde izquierdo...
            else if (moveDirection.x < 0 && enemy.transform.position.x <= leftBoundary.position.x)
            {
                SwitchDirectionAndMoveDown();
                break;
            }
        }
    }

    private void SwitchDirectionAndMoveDown()
    {
        // Invierto la dirección.
        moveDirection.x *= -1;

        // Muevo el contenedor hacia abajo.
        Vector3 currentPos = enemyContainer.position;
        currentPos.y -= levelData.verticalSpeed;
        enemyContainer.position = currentPos;

        SoundManager_SpaceInvaders.Instance.Play(SoundType.Edge);
    }

    private void HandleShooting()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= levelData.enemyFireFrequency)
        {
            fireTimer = 0;
            // Elijo un enemigo al azar para que dispare.
            if (activeEnemies.Count > 0)
            {
                int randomIndex = Random.Range(0, activeEnemies.Count);
                GameObject shooter = activeEnemies[randomIndex];
                Instantiate(enemyBulletPrefab, shooter.transform.position, Quaternion.identity);
            }
        }
    }

    private void SpawnEnemies()
    {
        // Limpio enemigos de una oleada anterior.
        foreach (Transform child in enemyContainer)
        {
            Destroy(child.gameObject);
        }
        activeEnemies.Clear();

        // Creo la parrilla de enemigos.
        for (int row = 0; row < levelData.rows; row++)
        {
            for (int col = 0; col < levelData.columns; col++)
            {
                // Elijo el prefab del enemigo para esta fila.
                GameObject enemyPrefab = levelData.enemyPrefabs[row % levelData.enemyPrefabs.Length];

                // Calculo la posición y creo el enemigo.
                Vector3 position = new Vector3(col * 2, -row * 1.5f, 0);
                GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity, enemyContainer);
                activeEnemies.Add(newEnemy);

                // Le pasamos al nuevo enemigo una referencia a este manager.
                Enemy_SpaceInvaders enemyScript = newEnemy.GetComponent<Enemy_SpaceInvaders>();
                if (enemyScript != null)
                {
                    enemyScript.Setup(this);
                }
            }
        }
    }

    // Los enemigos llamarán a esto cuando mueran.
    public void EnemyWasDestroyed(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        currentSpeed += levelData.enemySpeedIncrease;

        // Si ya no quedan enemigos, el jugador gana.
        if (activeEnemies.Count == 0)
        {
            gameManager.TriggerGameOver(true);
        }
    }
}