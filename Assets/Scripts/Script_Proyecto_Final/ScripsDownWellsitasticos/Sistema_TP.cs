// SimpleTeleporter.cs
using UnityEngine;

public class Sistema_TP : MonoBehaviour
{
    [Header("Configuraci�n del Teletransporte")]
    [Tooltip("Arrastra aqu� un objeto vac�o de la escena que marcar� el punto de destino.")]
    public Transform destination; // El objeto cuya posici�n usaremos como destino.

    [Header("Configuraci�n de la Interacci�n")]
    public KeyCode interactionKey = KeyCode.W; // La tecla principal para interactuar.

    // Variables privadas para saber si el jugador est� en la zona.
    private bool playerInRange = false;
    private GameObject playerObject; // Para guardar la referencia al jugador.

    // Este m�todo se ejecuta cuando algo entra en el trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si lo que ha entrado es el jugador (usando su Tag).
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Marcamos que el jugador est� en rango.
            playerObject = other.gameObject; // Guardamos el objeto del jugador.
            Debug.Log("Jugador ha entrado en el rango del teletransportador.");
            // Opcional: Podr�as mostrar aqu� un indicador visual (ej: "Pulsa W para entrar").
        }
    }

    // Este m�todo se ejecuta cuando algo sale del trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Comprobamos si es el jugador quien ha salido.
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Marcamos que el jugador ya no est� en rango.
            playerObject = null; // Borramos la referencia.
            Debug.Log("Jugador ha salido del rango del teletransportador.");
            // Opcional: Ocultar el indicador visual.
        }
    }

    // Update se ejecuta en cada fotograma.
    private void Update()
    {
        // Si el jugador est� en rango Y pulsa la tecla 'W' O la flecha hacia arriba...
        if (playerInRange && (Input.GetKeyDown(interactionKey) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        // Una comprobaci�n de seguridad para evitar errores si no hemos asignado un destino.
        if (destination == null)
        {
            Debug.LogError("�No se ha asignado un destino para este teletransportador!", this);
            return;
        }

        // �La magia! Movemos la posici�n del jugador a la posici�n del objeto de destino.
        playerObject.transform.position = destination.position;
        Debug.Log($"Jugador teletransportado a la posici�n de '{destination.name}'.");
    }
}