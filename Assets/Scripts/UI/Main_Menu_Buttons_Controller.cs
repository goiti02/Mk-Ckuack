using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Buttons_Controller : MonoBehaviour
{
    public void IndexLoad(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("El indice introducido es incorrecto");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("*Sale epicamente del juego*");
    }
}
