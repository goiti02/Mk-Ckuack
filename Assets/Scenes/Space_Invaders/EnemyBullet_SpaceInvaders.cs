using UnityEngine;

public class EnemyBullet_SpaceInvaders : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 4f;

    private bool isAlive = true;

    void Start()
    {
        // La bala se destruirá automáticamente después de 'lifetime' segundos si no choca con nada.
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Si la bala no ha chocado, la muevo hacia abajo.
        if (isAlive)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si ya he chocado, no hago nada más.
        if (!isAlive) return;

        // Compruebo si he chocado con el jugador o con una de sus balas.
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet") || other.CompareTag("Wall"))
        {
            isAlive = false;

            // Aquí podríamos dañar al jugador si choca con él.
            if (other.CompareTag("Player"))
            {
                // gameManager.LoseLife();
            }

            // Si choco con una bala del jugador, la destruyo también.
            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
            }

            // Toco un sonido de impacto.
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Hit);

            // Desactivo el collider y detengo el movimiento.
            GetComponent<Collider2D>().enabled = false;

            // Aquí podríamos activar una animación de explosión y destruir el objeto
            // cuando la animación termine. Por ahora, lo destruimos directamente.
            Destroy(gameObject);
        }
    }
}