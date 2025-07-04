using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject Panel_Pausa;

    void Update()
    {
        // Detecta si la tecla "Escape" (Esc) ha sido presionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        if (Panel_Pausa != null)
        {
            bool isPanelActive = Panel_Pausa.activeSelf;
            Panel_Pausa.SetActive(!isPanelActive);

            if (!isPanelActive)
            {
                Time.timeScale = 0f; // Pausa el juego
            }
            else
            {
                Time.timeScale = 1f; // Reanuda el juego
            }
        }
    }
}