using UnityEngine;

public class Bala : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bala ha impactado con un Enemigo.");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Suelo"))
        {
            Debug.Log("Bala ha impactado con el suelo.");
            Destroy(this.gameObject);
        }
    }
}