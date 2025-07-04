using UnityEngine;

public class Bullet_Logic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destruye al enemigo
            Destroy(collision.gameObject);

            // Destruye la bala
            Destroy(gameObject);
        }
    }
}
