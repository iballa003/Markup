
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private List<string> htmlTags = new List<string>() { "<strong>", "text", "</strong>" };
    private Dictionary<string, List<string>> htmlTags = new Dictionary<string, List<string>>();
    public GameObject buttonPrefab; // Asigna el Prefab del bot�n en el Inspector
    public GameObject slotPrefab; // Asigna el Prefab del bot�n en el Inspector
    public Transform parentPanel; // Donde se generar�n los botones (un Panel en el Canvas)
    public Vector2 rangoMin; // M�nima posici�n X e Y
    public Vector2 rangoMax; // M�xima posici�n X e Y
    public static SpawnManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        addElementsHTML();
        
        string firstElement = htmlTags.Keys.First();
        //GenerateSlots(randomKey);
        GenerateButtons(firstElement);
        
    }

    void addElementsHTML(){
        htmlTags.Add("strong", new List<string>() { "Pon el texto en negrita", "<strong>", "text", "</strong>", "<stromg>" });
        htmlTags.Add("p", new List<string>() { "Haz un parrafo", "<p>", "text", "</p>", "<pw>", "</pw>" });
        htmlTags.Add("a", new List<string>() { "Haz un enlace", "<a>", "text", "</‎a>", "<enlace>", "</enlace>" });
        htmlTags.Add("h1", new List<string>() { "¿Cuál es el que hace las letras más grande?", "<h1>", "text", "</h1>", "<h6>", "<h/6>" });
        htmlTags.Add("img", new List<string>() { "¿Cómo muestro una imagen?", "<img>", "src", ">", "scr", "<image>", "</img>" });
    }

    public void GenerateButtons(string randomKeyValue)
    {
        List<string> keys = new List<string>(htmlTags[randomKeyValue]);
        keys.RemoveAt(0);
        for (int i = 0; i < keys.Count; i++)
        {
            InstanciarBoton(keys[i]);
        }
    }

    public string GetFirstKey()
    {
        if (htmlTags.Count == 0) return null;
        return htmlTags.Keys.First();
    }

    public string GetFirstKeyText()
    {
        if (htmlTags.Count == 0) return null;
        return htmlTags[GetFirstKey()][0];
    }

    public void removeFirtsElement()
    {
        string firstElement = htmlTags.Keys.First();
        htmlTags.Remove(firstElement);
    }

    public void InstanciarBoton(string randomKeyValue)
    {
        // Generar posici�n aleatoria dentro del rango definido
        float posX = UnityEngine.Random.Range(rangoMin.x, rangoMax.x);
        float posY = UnityEngine.Random.Range(rangoMin.y, rangoMax.y);

        // Instanciar el bot�n
        GameObject nuevoBoton = Instantiate(buttonPrefab, parentPanel);

        // Ajustar la posici�n en la UI
        RectTransform rectTransform = nuevoBoton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(posX, posY);

        TextMeshProUGUI buttonText = nuevoBoton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = randomKeyValue; // Asigna el texto desde el array
        }
    }
}
