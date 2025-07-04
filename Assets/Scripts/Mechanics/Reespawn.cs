using UnityEngine;
using UnityEngine.SceneManagement; // Necesitamos este namespace para SceneManager

public class Reespawn : MonoBehaviour
{
    // Ya no necesitamos 'reespawn' (Transform) si vamos a cargar una escena
    // public Transform reespawn;

    // Puedes definir aqu� el nombre de la escena a cargar cuando el jugador "muere"
    [SerializeField] private string sceneToLoadOnDeath = "Basura";

    // Start is called before the first frame update
    void Start()
    {
        // No hay nada espec�fico que inicializar aqu� para este prop�sito
    }

    // Update is called once per frame
    void Update()
    {
        // No hay nada que actualizar continuamente para este prop�sito
    }

    // Usaremos OnCollisionEnter2D o OnTriggerEnter2D si tu juego es 2D.
    // Si tu juego es 3D y usas colisiones sin trigger, OnCollisionEnter est� bien.
    // Si es un trigger (lo m�s com�n para "zonas de muerte"), usa OnTriggerEnter2D/3D.

    // Ejemplo para colisiones (f�sicas, no trigger):
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // Es mejor usar CompareTag para rendimiento
        {
            // En lugar de teletransportar:
            // other.transform.position = reespawn.transform.position;

            // Llamamos a la funci�n que carga la escena
            PlayerDiedAndLoadScene();
        }
    }

    // Si tu trampa de "reaparici�n" es un TRIGGER (lo m�s com�n para �reas de ca�da/muerte):
    /*
    private void OnTriggerEnter2D(Collider2D other) // Si tu juego es 2D
    {
        if (other.CompareTag("Player"))
        {
            PlayerDiedAndLoadScene();
        }
    }

    private void OnTriggerEnter(Collider other) // Si tu juego es 3D y el collider es un trigger
    {
        if (other.CompareTag("Player"))
        {
            PlayerDiedAndLoadScene();
        }
    }
    */


    // Este m�todo encapsula la l�gica de lo que pasa cuando el jugador "muere" por esta trampa
    private void PlayerDiedAndLoadScene()
    {
        Debug.Log("Jugador ha ca�do en la zona de 'reespawn'. Cargando escena: " + sceneToLoadOnDeath);
        // Carga la escena especificada
        SceneManager.LoadScene(sceneToLoadOnDeath);

        // Opcional: Si tienes un GameManager que maneja vidas y quieres restarle una vida
        // y quiz�s activar un panel de Game Over solo si no quedan vidas, podr�as llamar a:
        // GameLogicManager_Gallina.Instance.PlayerHasDied();
        // Pero para la simplicidad de tu pedido actual, solo cargamos la escena.
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reespawn : MonoBehaviour
{
    public Transform reespawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = reespawn.transform.position;
        }
    }
}
*/