using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slot[] slots; // Referencia a todos los slots en la escena
    public Button levelButton;
    private Button checkButton;
    private Button nextButton;
    public GameObject timer;
    public GameObject helpWindow;
    public static GameManager instance;
    public bool winLevel;
    public bool activeGame;
    private int levelNumber;
    void Awake()
    {
        winLevel = false;
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
        
        helpWindow.GetComponentInChildren<TextMeshProUGUI>().text = "Pon el texto en negrita";
        helpWindow.SetActive(true);
        updateLevel();
        checkButton = GameObject.FindWithTag("CheckButton").GetComponent<Button>();
        nextButton = GameObject.FindWithTag("NextButton").GetComponent<Button>();
        
    }

    public void PressCheckButton()
    {
        checkButton.interactable = false;
        StartCoroutine(CheckAllSlots());
    }

    public void PressOkButton()
    {
        
        helpWindow.SetActive(false);
        checkButton.interactable=true;
        
        if(SpawnManager.instance.GetFirstKey() != null) { 
            activeGame = true;
        }else{
            SceneManager.LoadScene("Title");
        }
    }

    public void PressNextButton()
    {
        SpawnManager.instance.removeFirtsElement();
        updateLevel();
        resetButtons();
        destroyHTMLTags();
        winLevel = false;
        if(SpawnManager.instance.GetFirstKey() != null) { 
            string firstElement = SpawnManager.instance.GetFirstKey();
            SpawnManager.instance.GenerateButtons(firstElement);
            helpWindow.GetComponentInChildren<TextMeshProUGUI>().text = SpawnManager.instance.GetFirstKeyText();
            helpWindow.SetActive(true);
            modifierSlotTag();
        }
        else
        {
            timer.GetComponent<Timer>().PauseTimer();
            string timerText = timer.GetComponent<Timer>().textoTemporizador.text;
            activeGame = false;
            helpWindow.GetComponentInChildren<TextMeshProUGUI>().text = "¡Felicidades!\n Tiempo empleado: "+timerText;
            helpWindow.SetActive(true);
        }
    }

    public void modifierSlotTag() {
        Slot slot1 = slots[0];
        Slot slot2 = slots[1];
        Slot slot3 = slots[2];
        if (SpawnManager.instance.GetFirstKey() == "strong")
        {
            slot1.expectedTag = "<strong>";
            slot2.expectedTag = "text";
            slot3.expectedTag = "</strong>";
        }
        if (SpawnManager.instance.GetFirstKey() == "p")
        {
            slot1.expectedTag = "<p>";
            slot2.expectedTag = "text";
            slot3.expectedTag = "</p>";
        }
        if (SpawnManager.instance.GetFirstKey() == "a")
        {
            slot1.expectedTag = "<a>";
            slot2.expectedTag = "text";
            slot3.expectedTag = "</‎a>";
        }
        if (SpawnManager.instance.GetFirstKey() == "h1")
        {
            slot1.expectedTag = "<h1>";
            slot2.expectedTag = "text";
            slot3.expectedTag = "</h1>";
        }
        if (SpawnManager.instance.GetFirstKey() == "img")
        {
            slot1.expectedTag = "<img>";
            slot2.expectedTag = "src";
            slot3.expectedTag = ">";
        }
    }

    public void updateLevel()
    {
        levelNumber += 1;
        levelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Nivel: "+ levelNumber;
    }

    public void resetButtons()
    {
        checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Comprobar";
        nextButton.interactable = false;
        checkButton.interactable = true;
        ChangeButtonColor(Color.white);
    }
    IEnumerator CheckAllSlots()
    {
        bool isCorrect = true;

        foreach (Slot slot in slots)
        {
            // Verifica si hay un objeto en el slot
            Debug.Log("El slot " + slot.name + " está vacío.");
            if (slot.GetCurrentElement() == null)
            {
                isCorrect = false;
                Debug.Log("El slot " + slot.name + " está vacío.");
                break;
            }

            // Obtiene el objeto arrastrado en el slot
            DraggableUI draggable = slot.GetCurrentElement().GetComponent<DraggableUI>();

            if (draggable == null || draggable.GetButtonText() != slot.expectedTag)
            {
                isCorrect = false;
                Debug.Log("El objeto en " + slot.name + " no es el correcto.");
                break;
            }
        }

        if (isCorrect)
        {
            AudioManager.instance.PlayCorrect();
            winLevel = true;
            Debug.Log("¡Orden correcto!");
            ChangeButtonColor(Color.green);
            checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Correcto";
            nextButton.interactable = true;
        }
        else
        {
            //AudioManager.instance.PlayBuzzer();
            Debug.Log("Orden incorrecto. Inténtalo de nuevo.");
            ChangeButtonColor(Color.red);
            checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Incorrecto";
            yield return new WaitForSeconds(2f);
            ChangeButtonColor(Color.white);
            checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Comprobar";
            checkButton.interactable = true;
        }
    }
    public void ChangeButtonColor(Color newColor)
    {
        checkButton.GetComponent<Image>().color = newColor;
        
    }

    public void destroyHTMLTags()
    {
        GameObject[] botones = GameObject.FindGameObjectsWithTag("HTMLTag"); // Buscar todos los objetos con el tag

        foreach (GameObject boton in botones)
        {
            Destroy(boton); // Destruir cada bot�n encontrado
        }
    }
}