using UnityEngine;

public class Enemy_SpaceInvaders : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo en segundos que dura la animación de muerte antes de desaparecer.")]
    public float deathAnimationDuration = 1f;

    // Referencias
    private EnemyManager_SpaceInvaders enemyManager;
    private Animator animator;
    private bool isAlive = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Setup(EnemyManager_SpaceInvaders manager)
    {
        enemyManager = manager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive || !other.CompareTag("PlayerBullet"))
        {
            return;
        }

        // Marco que ya no estoy vivo para no morir dos veces.
        isAlive = false;

        // Desactivo mi collider para no recibir más impactos.
        GetComponent<Collider2D>().enabled = false;

        // Aviso al manager que me han destruido.
        if (enemyManager != null)
        {
            enemyManager.EnemyWasDestroyed(this.gameObject);
        }

        // Toco el sonido de la explosión.
        SoundManager_SpaceInvaders.Instance.Play(SoundType.Explosion);

        // Destruyo la bala del jugador que me ha dado.
        Destroy(other.gameObject);

        // Activo la animación de explosión, si existe.
        if (animator != null)
        {
            animator.SetTrigger("OnDeath");
        }

        // ---- CORRECCIÓN CLAVE ----
        // Destruyo el objeto del enemigo después de un pequeño retardo.
        // Esto le da tiempo a la animación de explosión a reproducirse.
        Destroy(gameObject, deathAnimationDuration);
    }
}