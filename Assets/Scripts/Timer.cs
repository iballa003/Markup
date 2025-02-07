using UnityEngine;
using TMPro; // Para TextMeshPro

public class Timer : MonoBehaviour
{
    public float time = 0f; // Asegurar que comienza en 0
    public TextMeshProUGUI textoTemporizador;
    private bool going = true;

    void Start()
    {
        time = 0f; // Reinicia el tiempo expl�citamente al inicio
        UpdateText();
    }

    void Update()
    {
        if (going && GameManager.instance.activeGame == true)
        {
            time += Time.deltaTime; // Sumar el tiempo correctamente
            UpdateText();
        }
    }

    void UpdateText()
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        textoTemporizador.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseTimer()
    {
        going = false;
    }

    public void ResumeTimer()
    {
        going = true;
    }

    public void ResetTimer()
    {
        time = 0f; // Reiniciar correctamente
        going = true;
        UpdateText();
    }
}

