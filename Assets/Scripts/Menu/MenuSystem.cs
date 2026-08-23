using System;
using Unity.PlayMode.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuSystem : MonoBehaviour
{
    public void jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
