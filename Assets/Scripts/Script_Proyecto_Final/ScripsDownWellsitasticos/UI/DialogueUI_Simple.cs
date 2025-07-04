// DialogueUI_Simple.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI_Simple : MonoBehaviour
{
    [Header("Componentes de la UI")]
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;
    public Image avatarPersonaje;
    public GameObject panelOpciones;
    public Button botonOpcion1;
    public Button botonOpcion2;

    // Método para mostrar el cuadro de diálogo con un texto y un avatar
    public void MostrarDialogo(string texto, Sprite avatar)
    {
        panelDialogo.SetActive(true);
        panelOpciones.SetActive(false); // Ocultamos las opciones por si estaban visibles
        textoDialogo.text = texto;
        avatarPersonaje.sprite = avatar;
    }

    // Método para mostrar las opciones con sus textos
    public void MostrarOpciones(string opcion1, string opcion2)
    {
        panelOpciones.SetActive(true);
        botonOpcion1.GetComponentInChildren<TextMeshProUGUI>().text = opcion1;
        botonOpcion2.GetComponentInChildren<TextMeshProUGUI>().text = opcion2;
    }

    // Método para ocultar toda la interfaz de diálogo
    public void OcultarTodo()
    {
        panelDialogo.SetActive(false);
        panelOpciones.SetActive(false);
    }
}