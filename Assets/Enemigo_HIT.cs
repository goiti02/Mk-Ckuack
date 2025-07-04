using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_HIt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // PASO 1: Asegurarnos de que el objeto que colisiona sea el jugador.
        // Es mejor usar CompareTag para rendimiento y seguridad.
        if (collision.gameObject.CompareTag("Player"))
        {
            // PASO 2: Intentar obtener el componente PlayerController.
            // Guardamos la referencia en una variable temporal.
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            // PASO 3: Verificar si realmente se encontr� el componente PlayerController.
            if (player != null)
            {
                // Si el componente existe, entonces llamamos a Respawn().
                player.Respawn();
            }
            else
            {
                // Si no se encontr� el PlayerController, logueamos un error
                // para saber qu� GameObject colision� que no era el esperado o le faltaba el script.
                Debug.LogError("El objeto colisionado con etiqueta 'Player' no tiene el componente PlayerController. " +
                               "Objeto: " + collision.gameObject.name, this);
            }
        }
        // Opcional: Si colisiona con algo que NO es el jugador, no hacemos nada o logueamos otra cosa.
        // else
        // {
        //     Debug.Log("Colisi�n con un objeto que no es el jugador: " + collision.gameObject.name);
        // }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_HIt : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().Respawn();
    }

}

*/
