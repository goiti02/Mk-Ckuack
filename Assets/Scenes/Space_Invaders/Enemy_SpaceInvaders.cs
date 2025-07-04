using UnityEngine;

public class Enemy_SpaceInvaders : MonoBehaviour
{
    [Header("Configuraci�n")]
    [Tooltip("Tiempo en segundos que dura la animaci�n de muerte antes de desaparecer.")]
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

        // Desactivo mi collider para no recibir m�s impactos.
        GetComponent<Collider2D>().enabled = false;

        // Aviso al manager que me han destruido.
        if (enemyManager != null)
        {
            enemyManager.EnemyWasDestroyed(this.gameObject);
        }

        // Toco el sonido de la explosi�n.
        SoundManager_SpaceInvaders.Instance.Play(SoundType.Explosion);

        // Destruyo la bala del jugador que me ha dado.
        Destroy(other.gameObject);

        // Activo la animaci�n de explosi�n, si existe.
        if (animator != null)
        {
            animator.SetTrigger("OnDeath");
        }

        // ---- CORRECCI�N CLAVE ----
        // Destruyo el objeto del enemigo despu�s de un peque�o retardo.
        // Esto le da tiempo a la animaci�n de explosi�n a reproducirse.
        Destroy(gameObject, deathAnimationDuration);
    }
}