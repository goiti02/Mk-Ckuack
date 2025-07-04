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

    // M�todo para mostrar el cuadro de di�logo con un texto y un avatar
    public void MostrarDialogo(string texto, Sprite avatar)
    {
        panelDialogo.SetActive(true);
        panelOpciones.SetActive(false); // Ocultamos las opciones por si estaban visibles
        textoDialogo.text = texto;
        avatarPersonaje.sprite = avatar;
    }

    // M�todo para mostrar las opciones con sus textos
    public void MostrarOpciones(string opcion1, string opcion2)
    {
        panelOpciones.SetActive(true);
        botonOpcion1.GetComponentInChildren<TextMeshProUGUI>().text = opcion1;
        botonOpcion2.GetComponentInChildren<TextMeshProUGUI>().text = opcion2;
    }

    // M�todo para ocultar toda la interfaz de di�logo
    public void OcultarTodo()
    {
        panelDialogo.SetActive(false);
        panelOpciones.SetActive(false);
    }
}