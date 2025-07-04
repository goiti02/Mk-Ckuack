using UnityEngine;

public class EnemyBullet_SpaceInvaders : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 4f;

    private bool isAlive = true;

    void Start()
    {
        // La bala se destruir� autom�ticamente despu�s de 'lifetime' segundos si no choca con nada.
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
        // Si ya he chocado, no hago nada m�s.
        if (!isAlive) return;

        // Compruebo si he chocado con el jugador o con una de sus balas.
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet") || other.CompareTag("Wall"))
        {
            isAlive = false;

            // Aqu� podr�amos da�ar al jugador si choca con �l.
            if (other.CompareTag("Player"))
            {
                // gameManager.LoseLife();
            }

            // Si choco con una bala del jugador, la destruyo tambi�n.
            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
            }

            // Toco un sonido de impacto.
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Hit);

            // Desactivo el collider y detengo el movimiento.
            GetComponent<Collider2D>().enabled = false;

            // Aqu� podr�amos activar una animaci�n de explosi�n y destruir el objeto
            // cuando la animaci�n termine. Por ahora, lo destruimos directamente.
            Destroy(gameObject);
        }
    }
}