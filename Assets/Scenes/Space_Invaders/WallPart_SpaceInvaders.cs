using UnityEngine;

public class WallPart_SpaceInvaders : MonoBehaviour
{
    // Aquí arrastraremos el prefab del efecto de partículas de la explosión verde.
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reviso si lo que me ha dado es una bala (del jugador o de un enemigo).
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            // Toco el sonido de impacto.
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Hit);

            // Creo el efecto de la explosión en mi posición.
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Destruyo la bala que me ha dado.
            Destroy(other.gameObject);

            // Y me destruyo a mí mismo.
            Destroy(gameObject);
        }
    }
}