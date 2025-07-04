// GatitoInteraction.cs (Versión Automática con Debug.Log)
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatitoInteraction : MonoBehaviour
{
    [Header("Referencias")]
    public DialogueUI_Simple uiManager;
    public string nombreEscenaCamion = "Nivel_Camion";

    [Header("Contenido del Diálogo")]
    public Sprite avatarNormal;
    public Sprite avatarFeliz;
    public Sprite avatarTriste;

    private int pasoConversacion = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pasoConversacion == 0)
        {
            // --- LÍNEA AÑADIDA PARA DEPURAR ---
            Debug.Log("¡Player detectado por el gatito! Se debería activar el panel y empezar el diálogo.");
            // ------------------------------------

            // Empezar la conversación
            pasoConversacion = 1;
            ActualizarDialogo();

            GetComponent<Collider2D>().enabled = false;
        }
    }

    // ... (El resto del script es idéntico)
    void ActualizarDialogo()
    {
        switch (pasoConversacion)
        {
            case 1:
                string textoPregunta = "He creado un nivel muy sencillo de un camión en una pista. ¿Quieres probarlo?\n" + // Salto de línea aquí
                           "Si después quieres regresar aquí, solo tienes que caerte por el precipicio y volverás a este lugar. Te estaré esperando."; // Segunda línea

                uiManager.MostrarDialogo(textoPregunta, avatarNormal);
                uiManager.MostrarOpciones("¡Sí, claro!", "Ahora no...");

                uiManager.botonOpcion1.onClick.RemoveAllListeners();
                uiManager.botonOpcion1.onClick.AddListener(JugadorDiceSi);

                uiManager.botonOpcion2.onClick.RemoveAllListeners();
                uiManager.botonOpcion2.onClick.AddListener(JugadorDiceNo);
                break;
            case 2:
                string textoSi = "¡De verdad quieres verlo? ¡Gracias!";
                uiManager.MostrarDialogo(textoSi, avatarFeliz);
                Invoke("CargarNivelCamion", 2.5f);
                break;
            case 3:
                string textoNo = "Está bien...";
                uiManager.MostrarDialogo(textoNo, avatarTriste);
                Invoke("CerrarDialogo", 2f);
                break;
        }
    }
    void JugadorDiceSi() { pasoConversacion = 2; ActualizarDialogo(); }
    void JugadorDiceNo() { pasoConversacion = 3; ActualizarDialogo(); }
    void CargarNivelCamion() { SceneManager.LoadScene(nombreEscenaCamion); }
    void CerrarDialogo() { uiManager.OcultarTodo(); }
}