// GatitoInteraction.cs (Versi�n Autom�tica con Debug.Log)
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatitoInteraction : MonoBehaviour
{
    [Header("Referencias")]
    public DialogueUI_Simple uiManager;
    public string nombreEscenaCamion = "Nivel_Camion";

    [Header("Contenido del Di�logo")]
    public Sprite avatarNormal;
    public Sprite avatarFeliz;
    public Sprite avatarTriste;

    private int pasoConversacion = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pasoConversacion == 0)
        {
            // --- L�NEA A�ADIDA PARA DEPURAR ---
            Debug.Log("�Player detectado por el gatito! Se deber�a activar el panel y empezar el di�logo.");
            // ------------------------------------

            // Empezar la conversaci�n
            pasoConversacion = 1;
            ActualizarDialogo();

            GetComponent<Collider2D>().enabled = false;
        }
    }

    // ... (El resto del script es id�ntico)
    void ActualizarDialogo()
    {
        switch (pasoConversacion)
        {
            case 1:
                string textoPregunta = "He creado un nivel muy sencillo de un cami�n en una pista. �Quieres probarlo?\n" + // Salto de l�nea aqu�
                           "Si despu�s quieres regresar aqu�, solo tienes que caerte por el precipicio y volver�s a este lugar. Te estar� esperando."; // Segunda l�nea

                uiManager.MostrarDialogo(textoPregunta, avatarNormal);
                uiManager.MostrarOpciones("�S�, claro!", "Ahora no...");

                uiManager.botonOpcion1.onClick.RemoveAllListeners();
                uiManager.botonOpcion1.onClick.AddListener(JugadorDiceSi);

                uiManager.botonOpcion2.onClick.RemoveAllListeners();
                uiManager.botonOpcion2.onClick.AddListener(JugadorDiceNo);
                break;
            case 2:
                string textoSi = "�De verdad quieres verlo? �Gracias!";
                uiManager.MostrarDialogo(textoSi, avatarFeliz);
                Invoke("CargarNivelCamion", 2.5f);
                break;
            case 3:
                string textoNo = "Est� bien...";
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