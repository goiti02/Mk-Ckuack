using UnityEngine;

public class Enemigo_HIT : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            Debug.Log("¡Enemigo golpeado por una bala! Destruyendo ambos objetos.");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_HIT : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().Respawn();
    }

}
*/