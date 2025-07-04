using UnityEngine;

public class Boton_CerrarJuego : MonoBehaviour
{

    public void ExitGame()
    {
        
        Application.Quit();
        Debug.Log("¡El juego se está cerrando!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
