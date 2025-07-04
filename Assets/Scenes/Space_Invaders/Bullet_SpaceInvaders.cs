using UnityEngine;
using System;

public class Bullet_SpaceInvaders : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    public static event Action OnBulletDestroyed;

    void Start()
    {
        // La bala se destruye sola después de un tiempo.
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mueve la bala hacia arriba.
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Se llama justo antes de que el objeto se destruya (por tiempo o por colisión).
    private void OnDestroy()
    {
        // Aviso de que la bala se ha destruido.
        OnBulletDestroyed?.Invoke();
    }

    // ÚNICA función para gestionar las colisiones.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reviso si la bala choca con un enemigo o un muro.
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            // La lógica de dañar al enemigo está en el propio script del enemigo.
            // Aquí, la bala solo tiene que destruirse.
            Destroy(gameObject);
        }
    }
}