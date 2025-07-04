using UnityEngine;

public class PlayerController_SpaceInvaders : MonoBehaviour
{
    [Header("Referencias")]
    public GameManager_SpaceInvaders gameManager;
    public PlayerHUD_SpaceInvaders playerHud;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform leftBoundary;
    public Transform rightBoundary;

    [Header("Atributos")]
    public float speed = 10f;
    public int lives = 3;
    public int maxBullets = 1;

    private int bulletsOnScreen = 0;

    // Suscripción a eventos.
    private void OnEnable()
    {
        Bullet_SpaceInvaders.OnBulletDestroyed += HandleBulletDestroyed;
        if (playerHud != null)
        {
            playerHud.UpdateAmmoDisplay(maxBullets - bulletsOnScreen);
            playerHud.UpdateLives(lives);
        }
    }

    private void OnDisable()
    {
        Bullet_SpaceInvaders.OnBulletDestroyed -= HandleBulletDestroyed;
    }

    void Update()
    {
        if (gameManager.CurrentState != GameState.Playing) return;

        // --- Movimiento ---
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * moveInput * speed * Time.deltaTime);

        // --- Límites ---
        float clampedX = Mathf.Clamp(transform.position.x, leftBoundary.position.x, rightBoundary.position.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // --- Disparo (Línea Corregida) ---
        // Ahora comprueba (espacio O ratón) Y LUEGO comprueba el límite de balas.
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && bulletsOnScreen < maxBullets)
        {
            Fire();
        }
    }

    private void Fire()
    {
        bulletsOnScreen++;
        if (playerHud != null) playerHud.UpdateAmmoDisplay(maxBullets - bulletsOnScreen);
        SoundManager_SpaceInvaders.Instance.Play(SoundType.Fire);
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    // El evento de la bala llama a esto para que sepamos que podemos volver a disparar.
    private void HandleBulletDestroyed()
    {
        bulletsOnScreen--;
        if (playerHud != null) playerHud.UpdateAmmoDisplay(maxBullets - bulletsOnScreen);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Enemy"))
        {
            lives--;
            if (playerHud != null) playerHud.UpdateLives(lives);
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Explosion);

            if (lives <= 0)
            {
                gameManager.TriggerGameOver(false);
                gameObject.SetActive(false);
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bonus"))
        {
            maxBullets++;
            if (playerHud != null) playerHud.UpdateAmmoDisplay(maxBullets - bulletsOnScreen);
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Pickup);
            Destroy(other.gameObject);
        }
    }
}