using UnityEngine;
using System;

public class Bullet_SpaceInvaders : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    public static event Action OnBulletDestroyed;

    void Start()
    {
        // La bala se destruye sola despu�s de un tiempo.
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mueve la bala hacia arriba.
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Se llama justo antes de que el objeto se destruya (por tiempo o por colisi�n).
    private void OnDestroy()
    {
        // Aviso de que la bala se ha destruido.
        OnBulletDestroyed?.Invoke();
    }

    // �NICA funci�n para gestionar las colisiones.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reviso si la bala choca con un enemigo o un muro.
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            // La l�gica de da�ar al enemigo est� en el propio script del enemigo.
            // Aqu�, la bala solo tiene que destruirse.
            Destroy(gameObject);
        }
    }
}