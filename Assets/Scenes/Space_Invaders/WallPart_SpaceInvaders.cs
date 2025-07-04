using UnityEngine;

public class WallPart_SpaceInvaders : MonoBehaviour
{
    // Aqu� arrastraremos el prefab del efecto de part�culas de la explosi�n verde.
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reviso si lo que me ha dado es una bala (del jugador o de un enemigo).
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            // Toco el sonido de impacto.
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Hit);

            // Creo el efecto de la explosi�n en mi posici�n.
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Destruyo la bala que me ha dado.
            Destroy(other.gameObject);

            // Y me destruyo a m� mismo.
            Destroy(gameObject);
        }
    }
}