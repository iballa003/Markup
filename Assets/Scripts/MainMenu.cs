using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() // Opcional: Cambiar a may�scula por convenci�n
    {
        if (SceneManager.GetSceneByName("Game") != null)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.LogError("La escena 'Game' no est� en Build Settings o el nombre es incorrecto.");
        }
    }
}

