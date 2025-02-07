using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() // Opcional: Cambiar a mayúscula por convención
    {
        if (SceneManager.GetSceneByName("Game") != null)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.LogError("La escena 'Game' no está en Build Settings o el nombre es incorrecto.");
        }
    }
}

